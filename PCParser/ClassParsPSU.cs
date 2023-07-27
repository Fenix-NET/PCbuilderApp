﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PCParser.DTOs;

namespace PCParser
{
    public class ClassParsPSU
    {

        public async Task StartParsePSU()
        {

            Console.WriteLine("Подготовка парсера");

            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://tula.nix.ru/price.html?section=power_supplies_all#c_id=772&fn=772&g_id=927&page=1&sort=%2Bp6920%2B127%2B998%2B2289&spoiler=&store=region-1483_0&thumbnail_view=2";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var urlSelector = "a.t"; 
            var cells = document.QuerySelectorAll(urlSelector).OfType<IHtmlAnchorElement>();
            var titlesRef = cells.Select(m => m.Href).ToList();
            var priceSelector = "td.d.tac.cell-price > span"; 
            var cellsP = document.QuerySelectorAll(priceSelector);

            var titlesPrice = cellsP.Select(m => m.TextContent.Replace(" ", "")).ToList();

            List<PSUParse> PSUs = new List<PSUParse>();

            int x = 0;
            Console.WriteLine("Начало парсинга PSU");

            var manufacturerSelector = "td#tdsa2943";
            var modelSelector = "td#tdsa2944";                 
            var powerSelector = "td#tdsa2123";

            IElement cellss;
            for (int i = 0; i < titlesRef.Count; i++)
            {
                PSUs.Add(new PSUParse());
                PSUs[x].Price = decimal.Parse(titlesPrice[i]);
                address = titlesRef[i];
                document = await context.OpenAsync(address);
             
                cellss = document.QuerySelector(manufacturerSelector);
                PSUs[x].Manufacturer = cellss?.TextContent ?? cellss?.FirstChild.TextContent ?? "n/a";

                cellss = document.QuerySelector(modelSelector);
                PSUs[x].Model = cellss?.FirstChild.TextContent ?? cellss?.TextContent ?? "n/a";

                cellss = document.QuerySelector(powerSelector);
                PSUs[x].Power = ushort.Parse(Regex.Replace(cellss.TextContent, @"\D+", ""));

                x++;
            }
            Console.WriteLine("Конец работы");
            for (int i = 0; i < PSUs.Count; i++)
            {
                Console.WriteLine($"Производитель : {PSUs[i].Manufacturer}");
                Console.WriteLine($"Модель : {PSUs[i].Model}");
                Console.WriteLine($"Мощность : {PSUs[i].Power}");
                Console.WriteLine($"Цена : {PSUs[i].Price}");
                Console.WriteLine("================================================================");
            }


        }


    }
}