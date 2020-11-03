using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using RDAT.ViewModels;
using RDAT.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RDAT.Controllers
{
    public class CertificateController : Controller
    {

        public IActionResult Create()
        {
            CertificateViewModel _model = new CertificateViewModel();

            _model.Companies = GetCompanies();

            return View(_model);
        }

        public List<SelectListItem> GetCompanies()
        {
            using RDATContext context = new RDATContext();
            List<SelectListItem> companies = context.Companys.Where(c => c.isDelete == false).OrderBy(c => c.Name).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Name, //a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();

            return companies;
        }


        [HttpPost]
        public IActionResult Create(CertificateViewModel model)
        {
            string Name = model.Name;

            string _StartDate = "January 1, " + model.Year;
            string _EndDate = "December 31, " + model.Year;

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var htmlLoadOptions = new HtmlLoadOptions();
            
                // Build Doc from Scratch
                var document = new DocumentModel();
                Section section = new Section(document);
                document.Sections.Add(section);

                document.Content.Start
                    .LoadText("<div style='text-align: center;margin-top: 30px;'><img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/title_one.png' alt='Certificate Top' /></div>",
                    new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;font-size: 28px;font-weight: bold;padding: 20px;'>" + model.Name + "</div>", new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding: 10px;'>In accordance with Federal Motor Carrier Safety Regulations, Part 382, the above-named company is a member and full participant in a managed drug and alcohol testing program operated by:.</div>", new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding: 10px;'><img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/mid_one.png' alt='Certificate Top' /></div>",
                        new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding: 10px;'>Trucking Qualifications Services, LLC confirms to the rules set forth in 49 CFR, Part 40, Procedures for the Drug and Alcohol Testing Industry Association and all staff members are certified per the requirements of the federal regulations contained in the Code of Federal Register Part 40.33.</div>", new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding: 10px;'>All random selections percentage of at least 50% drug and at least 10% alcohol is being maintained in accordance with these regulations.</div>", new HtmlLoadOptions())
                    .LoadText("<table style='width: 100%;' cellpadding='5' cellspacing='5'><tr><td><strong>Trucking Qualifications Services LLC</strong><br>6250 Shiloh Road, Ste. 230<br>Alpharetta, Georgia 30005</td><td style='text-align: right;'>Program Start Date: " + _StartDate + "<br>Program End Date:  " + _EndDate + "</td></tr></table>", new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding-right: 30px;'><img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/slice_bottom.png' alt='Certificate Bottom' /></div>",
                        new HtmlLoadOptions());


                
                var pageSetup1 = document.Sections[0].PageSetup;

                // Set page orientation.
                pageSetup1.Orientation = Orientation.Landscape;

                // Set page margins.
                pageSetup1.PageMargins.Top = 10;
                pageSetup1.PageMargins.Bottom = 0;

                // Set paper type.
                pageSetup1.PaperType = PaperType.Letter;

                document.Save(@"wwwroot/Output.pdf");



            return File(@"Output.pdf", "application/pdf", "certificate.pdf");

        }

        [HttpPost]
        public FileResult CreatePDF(string CertHtml)
        {
            //using (Document document = new Document())
            //{
            //    // step 2
            //    PdfWriter.GetInstance(document, stream);
            //    // step 3
            //    document.Open();
            //    // step 4
            //    document.Add(new Paragraph("Hello World!"));
            //}

            var html = @"
                <html>
                <head>
                
                <style>
                  @page {
                    size: A5 landscape;
                    margin: 6cm 1cm 1cm;
                    mso-header-margin: 1cm;
                    mso-footer-margin: 1cm;
                    
                  }

                  body {
                    background: #FFF;
                    border: 1pt solid black;
                    padding: 20pt;
                    width: 700px;
                  }

                  br {
                    page-break-before: always;
                  }

                  p { margin: 0; }
                  header { color: #000; text-align: center; font-family: 'Tangerine', serif; font-size: 48px;}
                  main { color: #00B050; }
                  footer { color: #0070C0; text-align: right; }
                </style>
                </head>

                <body>
                  <header>
                    <img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/certificate_top.jpg' alt='Certificate Top'/>
                  </header>
                  <main>
<div style='text-align: center;'>Matthew Bowles</div>
<div style='text-align: center;'><img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/certificate_trucking.jpg' alt='TQS llc'/>
                  </main>
                  <footer>
                    <img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/certificate_footer_logo.jpg' alt='Certificate Top'/>
                  </footer>
                </body>
                </html>";

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var htmlLoadOptions = new HtmlLoadOptions();
            using (var htmlStream = new MemoryStream(htmlLoadOptions.Encoding.GetBytes(html)))
            {
                // Load input HTML text as stream.
                // var document = DocumentModel.Load(htmlStream, htmlLoadOptions);
                // Save output PDF file.

                // Build Doc from Scratch
                var document = new DocumentModel();
                Section section = new Section(document);
                document.Sections.Add(section);

                document.Content.Start
                    .LoadText("<div style='text-align: center;margin-top: 30px;'><img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/title_one.png' alt='Certificate Top' /></div>",
                    new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;font-size: 28px;font-weight: bold;padding: 20px;'>Matthew Bowles</div>", new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding: 10px;'>In accordance with Federal Motor Carrier Safety Regulations, Part 382, the above-named company is a member and full participant in a managed drug and alcohol testing program operated by:.</div>", new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding: 10px;'><img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/mid_one.png' alt='Certificate Top' /></div>",
                        new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding: 10px;'>Trucking Qualifications Services, LLC confirms to the rules set forth in 49 CFR, Part 40, Procedures for the Drug and Alcohol Testing Industry Association and all staff members are certified per the requirements of the federal regulations contained in the Code of Federal Register Part 40.33.</div>", new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding: 10px;'>All random selections percentage of at least 50% drug and at least 10% alcohol is being maintained in accordance with these regulations.</div>", new HtmlLoadOptions())
                    .LoadText("<table style='width: 100%;' cellpadding='5' cellspacing='5'><tr><td><strong>Trucking Qualifications Services LLC</strong><br>6250 Shiloh Road, Ste. 230<br>Alpharetta, Georgia 30005</td><td style='text-align: right;'>Program Start Date: January 1, 2020<br>Program End Date: December 31, 2020</td></tr></table>", new HtmlLoadOptions())
                    .LoadText("<div style='text-align: center;padding-right: 30px;'><img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/slice_bottom.png' alt='Certificate Bottom' /></div>",
                        new HtmlLoadOptions());


                //var section = document.Sections[0];

                //Run run = new Run(document, "<img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/slice_title.png' alt='Certificate Top'/>");
                //paragraph.Inlines.Add(run);

                //section.Blocks[1].Content.Start.LoadText(" Some Prefix ", new CharacterFormat() { Subscript = true });

                // Append text to second paragraph.
                //section.Blocks[1].Content.End.LoadText(" Some Suffix ", new CharacterFormat() { Superscript = true });

                // Insert HTML paragraph before third paragraph.
                //section.Blocks[2].Content.Start.LoadText();

                var pageSetup1 = document.Sections[0].PageSetup;

                // Set page orientation.
                pageSetup1.Orientation = Orientation.Landscape;

                // Set page margins.
                pageSetup1.PageMargins.Top = 10;
                pageSetup1.PageMargins.Bottom = 0;

                // Set paper type.
                pageSetup1.PaperType = PaperType.Letter;

                document.Save(@"wwwroot/Output.pdf");
                
                
            }

            return File(@"Output.pdf", "application/pdf", "certificate.pdf");
            
        }

        public IActionResult Index()
        {
            
                // If using Professional version, put your serial key below.
                ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            
                var html = @"
                <html>
                <head>
                
                <style>
                  @page {
                    size: A5 landscape;
                    margin: 6cm 1cm 1cm;
                    mso-header-margin: 1cm;
                    mso-footer-margin: 1cm;
                    
                  }

                  body {
                    background: #FFF;
                    border: 1pt solid black;
                    padding: 20pt;
                    width: 700px;
                  }

                  br {
                    page-break-before: always;
                  }

                  p { margin: 0; }
                  header { color: #000; text-align: center; font-family: 'Tangerine', serif; font-size: 48px;}
                  main { color: #00B050; }
                  footer { color: #0070C0; text-align: right; }
                </style>
                </head>

                <body>
                  <header>
                    <img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/certificate_top.jpg' alt='Certificate Top'/>
                  </header>
                  <main>
<div style='text-align: center;'>Matthew Bowles</div>
<div style='text-align: center;'><img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/certificate_trucking.jpg' alt='TQS llc'/>
                  </main>
                  <footer>
                    <img src='https://rdat20200218122513.azurewebsites.net/theme/dist/img/certificate_footer_logo.jpg' alt='Certificate Top'/>
                  </footer>
                </body>
                </html>";

                var htmlLoadOptions = new HtmlLoadOptions();
                using (var htmlStream = new MemoryStream(htmlLoadOptions.Encoding.GetBytes(html)))
                {
                    // Load input HTML text as stream.
                    var document = DocumentModel.Load(htmlStream, htmlLoadOptions);
                // Save output PDF file.
                
                document.Save(@"wwwroot/Output.pdf");
                }
           
            return View();
        }
    }
}
