using ceTe.DynamicPDF;
using ceTe.DynamicPDF.LayoutEngine.LayoutElements;
using Project.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ceTe.DynamicPDF.PageElements;
using Label = ceTe.DynamicPDF.PageElements.Label;
using Line = ceTe.DynamicPDF.PageElements.Line;
using System.Windows;

namespace Project.ViewModel.Guest2ViewModel
{
    public class MyReservedTourInfoViewModel
    {
        private RelayCommand generatePdfCommand;
        public ICommand GeneratePdfCommand
        {
            get
            {
                if(generatePdfCommand == null)
                {
                    generatePdfCommand = new RelayCommand(param => this.GeneratePDF(), param => this.CanGeneratePDF());
                }
                return generatePdfCommand;
            }
        }

        private bool CanGeneratePDF()
        {
            return true;
        }

        private void GeneratePDF()
        {
            string content = "TourInfo" + "Subotica";
            string path = $"C:\\sims\\SIMS_GroupD\\Project\\Project\\Resources\\PDFReports\\{content}.pdf";

            Document document = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Landscape, 54.0f);
            document.Pages.Add(page);
            
            Label label = new Label("Tour info report", 0,0,504,100,Font.TimesRoman,23,TextAlign.Center);
            page.Elements.Add(label);

            Line line = new Line(0, 30, 504, 30);
            page.Elements.Add(line);

            Label nameLabel = new Label("Name: Obilazak Subotice",0,50, 504,40,Font.TimesRoman,13);
            Label cityLabel = new Label("City: Subotica", 0, 75, 504, 40, Font.TimesRoman, 13);
            Label countryLabel = new Label("Country: Srbija", 0, 100, 504, 40, Font.TimesRoman, 13);
            Label languageLabel = new Label("Language: Srpski", 0, 125, 504, 40, Font.TimesRoman, 13);
            Label durationLabel = new Label("Duration (in hours): 2", 0, 150, 504, 40, Font.TimesRoman, 13);
            Label guideLabel = new Label("Guide: Guide 1", 0, 175, 504, 40, Font.TimesRoman, 13);
            Label maxGuestsLabel = new Label("Max guests: 15", 0, 200, 504, 40, Font.TimesRoman, 13);


            page.Elements.Add(nameLabel); 
            page.Elements.Add(cityLabel);
            page.Elements.Add(countryLabel);
            page.Elements.Add(languageLabel);
            page.Elements.Add(durationLabel);
            page.Elements.Add(guideLabel);
            page.Elements.Add(maxGuestsLabel);

            document.Draw(path);
            MessageBox.Show("PDF report succesfully created");
        }
    }
}
