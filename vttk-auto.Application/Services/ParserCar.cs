using HtmlAgilityPack;
using vttk_auto.Application.Models.Cars;
using vttk_auto.Application.Models.Specifications;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Services;

public class ParserCar(HtmlWeb web)
{
    public async Task<CarEntity> ParserData(string url)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument document = await web.LoadFromWebAsync(url);
        
        var carsNodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'p-firm__text link-holder')]");
        
        foreach (var carNode in carsNodes)
        {
            var link = carNode.GetAttributeValue("href", "");
            var absoluteLink = new Uri(new Uri(url), link).AbsoluteUri;
            var title = carNode.InnerText;
            Console.WriteLine($"Title: {title}");
            
             HtmlDocument document1 = await web.LoadFromWebAsync(absoluteLink);
             
             var carMarks = document1.DocumentNode.SelectNodes("//a[contains(@class, 'p-car__title link-holder')]");
            
             foreach (var carMark in carMarks)
             {
                 var titleMark = carMark.InnerText;
                 //Console.WriteLine($"Mark: {titleMark}");
                
                 var linkToModel = carMark. GetAttributeValue("href", "");
                 var absoluteLinkToModel = new Uri(new Uri(absoluteLink), linkToModel).AbsoluteUri;
                 HtmlDocument document2 = await web.LoadFromWebAsync(absoluteLinkToModel);
                 var carsModify = document2.DocumentNode.SelectNodes("//a[contains(@class, 'text text_bold_medium')]");
                
                 foreach (var modify in carsModify)
                 {
                     var titleModify = modify.InnerText;
                     //Console.WriteLine($"TitleModify: {titleModify}");
                     
                     var linkToDetails = modify.GetAttributeValue("href", "");
                     var absoluteLinkToDetails = new Uri(new Uri(absoluteLinkToModel), linkToDetails).AbsoluteUri;
                     HtmlDocument document3 = await web.LoadFromWebAsync(absoluteLinkToDetails);
                     //length width weight
                     var carsWidth = document3.DocumentNode.SelectNodes("//span[contains(@class, 'text text_light_small color_black')]");
                
                     foreach (var carWidth in carsWidth)
                     {
                         //Console.WriteLine($"CarWidth: {carWidth.InnerText}");
                     }
                 }
            }
        }
    }


    public async Task<List<HtmlNode?>> GetMark(string url)
    {
        var listMarks = new List<HtmlNode?>();
        HtmlDocument document = await web.LoadFromWebAsync(url);
        var carsNodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'p-firm__text link-holder')]");
        foreach (var carNode in carsNodes)
        {
            listMarks.Add(carNode);
        }
        return listMarks;
    }
}
