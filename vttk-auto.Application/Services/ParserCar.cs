using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Services;

public class ParserCar
{
    public List<CarEntity> ParserCarModel()
    {
        var carEntities = new List<CarEntity>();
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-gpu");

        using var driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://www.drom.ru/catalog/");
        Thread.Sleep(500);

        try
        {
            var showAllButton = driver.FindElement(By.XPath("//div[contains(@class, 'css-10r1lxn e1cj4jfp4')]"));
            showAllButton.Click();
            Thread.Sleep(500);
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Кнопка 'Показать все' не найдена.");
        }

        var carLinks = driver.FindElements(By.CssSelector("a[data-ftid='component_cars-list-item_hidden-link']"))
            .Select(e => new { Brand = e.Text.Trim(), Link = e.GetAttribute("href") })
            .ToList();
        
        var firstBrand = carLinks.FirstOrDefault();
        if (firstBrand != null)
        {
            var car = firstBrand;
            driver.Navigate().GoToUrl(car.Link);
            Thread.Sleep(500);

            var carModels = driver.FindElements(By.CssSelector("a.g6gv8w4._501ok20"))
                .Select(cm => new { Model = cm.Text.Trim(), Link = cm.GetAttribute("href") })
                .ToList();

            foreach (var model in carModels)
            {
                driver.Navigate().GoToUrl(model.Link);
                Thread.Sleep(500);

                var generationLinks = driver.FindElements(By.CssSelector("a[data-ftid='component_article']"))
                    .Select(gl => new { GenTitle = gl.Text.Trim(), Link = gl.GetAttribute("href") })
                    .ToList();

                foreach (var generation in generationLinks)
                {
                    driver.Navigate().GoToUrl(generation.Link);
                    Thread.Sleep(500);

                    if (driver.PageSource.Contains("Извините, комплектаций автомобиля по заданным условиям не найдено"))
                        continue;

                    var complectations = driver.FindElements(By.CssSelector("tr[data-ftid='complectations-table-row']"))
                        .Select(tr =>
                        {
                            var tds = tr.FindElements(By.TagName("td"));
                            if (tds.Count > 1)
                            {
                                try
                                {
                                    var linkEl = tds[1].FindElement(By.TagName("a"));
                                    return new
                                    {
                                        Modification = linkEl.Text.Trim(),
                                        Link = linkEl.GetAttribute("href")
                                    };
                                }
                                catch (NoSuchElementException) { }
                            }
                            return null;
                        })
                        .Where(x => x != null)
                        .ToList();

                    foreach (var compl in complectations)
                    {
                        driver.Navigate().GoToUrl(compl.Link);
                        Thread.Sleep(500);

                        var specs = new Dictionary<string, string>();
                        var rows = driver.FindElements(By.CssSelector("table tr"));
                        foreach (var row in rows)
                        {
                            var cells = row.FindElements(By.TagName("td"));
                            if (cells.Count >= 2)
                            {
                                string name = cells[0].Text.Trim();
                                string value = cells[1].Text.Trim();
                                specs[name] = value;
                            }
                        }

                        if (specs.Count == 0) continue;

                        // Извлечение нужных параметров
                        specs.TryGetValue("Габариты кузова (Д x Ш x В), мм", out string sizeRaw);
                        specs.TryGetValue("Масса, кг", out string weight);
                        string length = "", width = "", height = "";

                        if (!string.IsNullOrEmpty(sizeRaw))
                        {
                            var sizes = sizeRaw.Split('x').Select(s => s.Trim()).ToArray();
                            if (sizes.Length == 3)
                            {
                                length = sizes[0];
                                width = sizes[1];
                                height = sizes[2];
                            }
                        }

                        var specifications = new SpecificationsEntity
                        {
                            Id = Guid.NewGuid(),
                            Length = length,
                            Width = width,
                            Height = height,
                            Weight = weight
                        };

                        var carEntity = new CarEntity
                        {
                            Id = Guid.NewGuid(),
                            Brand = car.Brand,
                            Model = model.Model,
                            Modification = compl.Modification,
                            SpecificationId = specifications.Id,
                            Specifications = specifications
                        };

                        specifications.CarId = carEntity.Id;
                        specifications.Car = carEntity;

                        carEntities.Add(carEntity);
                    }
                }
            }
        }

        return carEntities;
    }
}
