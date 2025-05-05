using HtmlAgilityPack;
using vttk_auto.Application.Models.Cars;
using vttk_auto.Application.Models.Specifications;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Services;

public class ParserCar(HtmlWeb web)
{
    public async Task<List<CarEntity>> ParserCarModel(string url = "https://auto.mail.ru/catalog/")
    {
        var carEntities = new List<CarEntity>();
        HtmlDocument document = await web.LoadFromWebAsync(url);

        var carsNodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'p-firm__text link-holder')]");

        foreach (var carNode in carsNodes)
        {
            string brand = carNode.InnerText.Trim();
            var link = carNode.GetAttributeValue("href", "");
            var absoluteLink = new Uri(new Uri(url), link).AbsoluteUri;

            HtmlDocument document1 = await web.LoadFromWebAsync(absoluteLink);
            var carMarks = document1.DocumentNode.SelectNodes("//a[contains(@class, 'p-car__title link-holder')]");
            if (carMarks == null) continue;

            foreach (var carMark in carMarks)
            {
                string model = carMark.InnerText.Trim();
                var linkToModel = carMark.GetAttributeValue("href", "");
                var absoluteLinkToModel = new Uri(new Uri(absoluteLink), linkToModel).AbsoluteUri;

                HtmlDocument document2 = await web.LoadFromWebAsync(absoluteLinkToModel);
                var carsModify = document2.DocumentNode.SelectNodes("//a[contains(@class, 'text text_bold_medium')]");
                if (carsModify == null) continue;

                foreach (var modify in carsModify)
                {
                    string modification = modify.InnerText.Trim();
                    var linkToDetails = modify.GetAttributeValue("href", "");
                    var absoluteLinkToDetails = new Uri(new Uri(absoluteLinkToModel), linkToDetails).AbsoluteUri;

                    HtmlDocument document3 = await web.LoadFromWebAsync(absoluteLinkToDetails);
                    var specNodes =
                        document3.DocumentNode.SelectNodes(
                            "//span[contains(@class, 'text text_light_small color_black')]");
                    if (specNodes == null || specNodes.Count < 3) continue;
                    int indexLength = specNodes.ToList().FindIndex(n => n.InnerText.Trim().ToLower() == "длина, мм");
                    int indexWidth= specNodes.ToList().FindIndex(n => n.InnerText.Trim().ToLower() == "ширина, мм");
                    int indexHeight = specNodes.ToList().FindIndex(n => n.InnerText.Trim().ToLower() == "высота, мм");
                    int indexWeight = specNodes.ToList().FindIndex(n => n.InnerText.Trim().ToLower() == "снаряженная масса, мм");

                    var specifications = new SpecificationsEntity
                    {
                        Id = Guid.NewGuid(),
                        Width = specNodes[indexWidth+1].InnerText.Trim(),
                        Height = specNodes[indexHeight+1].InnerText.Trim(),
                        Weight = specNodes[indexWeight+1].InnerText.Trim(),
                        Length = specNodes[indexLength+1].InnerText.Trim(),
                    };

                    var carEntity = new CarEntity
                    {
                        Id = Guid.NewGuid(),
                        Brand = brand,
                        Model = model,
                        Modification = modification,
                        SpecificationId = specifications.Id,
                        Specifications = specifications
                    };

                    specifications.CarId = carEntity.Id;
                    specifications.Car = carEntity;

                    carEntities.Add(carEntity);
                }
            }
        }

        return carEntities;
    }
}
