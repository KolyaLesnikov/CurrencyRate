using CurrencyRate.Infrastructure;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CurrencyRate.ViewModel
{
  public  class ViewModel
    {
        public ObservableCollection<Currency> Currencies { get; set; } = new ObservableCollection<Currency>();
        public ICommand UpdateCommand { get; set; }
     
        public ViewModel()
        {
            UpdateCommand = new RelayCommand(x => Get());
           
            Get();
        }
        public async  void Get()
        {
            Currencies = new ObservableCollection<Currency>();
            var url = @"https://www.finanz.ru/valyuty/v-realnom-vremeni";
            HttpClient client = new HttpClient();

            string html = await client.GetStringAsync(url);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);


            var currNames = document.DocumentNode.Descendants("a")
                .Where(x => x.GetAttributeValue("title", "")
                .Contains("/"))
                .ToList();
            currNames.RemoveRange(0, 3);
            var currValues = document.DocumentNode.Descendants("span")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("push-data "))
                .Where(x => x.InnerText != "-")
                .ToList();

            var currDate = document.DocumentNode
                .Descendants("span")
                .Where(x => x.GetAttributeValue("data-format", "")
                .Contains("utcToApplicationOffset:3;"))
                .ToList();
            
            for (int i = 0; i < currNames.Count; i++)
            {
                Currency newCurr = new Currency(currNames[i].InnerText, Double.Parse(currValues[i].InnerText),DateTime.Parse(currDate[i].InnerText));
                Currencies.Add(newCurr);
            }

            
        }
     
    }
}
