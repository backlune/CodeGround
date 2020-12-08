using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Annotations;
using System;
using System.IO;
using System.Linq;

namespace GodeGround.Security
{
    public class PdfDocumentScripting
    {

        public static MemoryStream AddAutoPrint(Stream pdfStream, bool ShowPrintDialog = true, int NumCopies = 1)
        {
            PdfSharp.Pdf.PdfDocument doc = PdfSharp.Pdf.IO.PdfReader.Open(pdfStream, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);
            PdfSharp.Pdf.PdfDocument outputDocument = new PdfSharp.Pdf.PdfDocument();

            for (int idx = 0; idx < doc.PageCount; idx++)
            {
                PdfSharp.Pdf.PdfPage p = doc.Pages[idx];
                outputDocument.AddPage(p);
            }

            outputDocument.Info.Author = "author name";

            string JSScript = string.Empty;
            JSScript += "var pp = this.getPrintParams(); ";

            if (NumCopies > 0)
            {
                JSScript += "pp.NumCopies = " + NumCopies.ToString() + "; ";
            }

            if (!ShowPrintDialog)
            {
                JSScript += "pp.interactive = pp.constants.interactionLevel.automatic; ";
            }


            JSScript += "this.print({printParams: pp}); ";


            PdfSharp.Pdf.PdfDictionary dictJS = new PdfSharp.Pdf.PdfDictionary();
            dictJS.Elements["/S"] = new PdfSharp.Pdf.PdfName("/JavaScript");
            //dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, "print(true);");
            //dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, "this.print({bUI: false, bSilent: true, bShrinkToFit: true});");
            //dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, "var pp = this.getPrintParams(); pp.NumCopies = 2; pp.interactive = pp.constants.interactionLevel.automatic; this.print({printParams: pp});");
            dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(outputDocument, JSScript);


            outputDocument.Internals.AddObject(dictJS);

            PdfSharp.Pdf.PdfDictionary dict = new PdfSharp.Pdf.PdfDictionary();
            PdfSharp.Pdf.PdfArray a = new PdfSharp.Pdf.PdfArray();
            dict.Elements["/Names"] = a;
            a.Elements.Add(new PdfSharp.Pdf.PdfString("EmbeddedJS"));
            a.Elements.Add(PdfSharp.Pdf.Advanced.PdfInternals.GetReference(dictJS));

            outputDocument.Internals.AddObject(dict);
            PdfSharp.Pdf.PdfPage p1 = doc.Pages[0];
            p1.Contents.Elements.Add(dict);

            PdfSharp.Pdf.PdfDictionary group = new PdfSharp.Pdf.PdfDictionary();
            group.Elements["/JavaScript"] = PdfSharp.Pdf.Advanced.PdfInternals.GetReference(dict);

            outputDocument.Internals.Catalog.Elements["/Names"] = group;

            MemoryStream ms = new MemoryStream();

            outputDocument.Save(ms, false);

            return ms;
        }

        public static MemoryStream AddAutoPrintOnPage(Stream pdfStream, bool ShowPrintDialog = true, int NumCopies = 1)
        {
            PdfSharp.Pdf.PdfDocument doc = PdfSharp.Pdf.IO.PdfReader.Open(pdfStream, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);
            PdfSharp.Pdf.PdfDocument outputDocument = new PdfSharp.Pdf.PdfDocument();

            for (int idx = 0; idx < doc.PageCount; idx++)
            {
                PdfSharp.Pdf.PdfPage p = doc.Pages[idx];
                outputDocument.AddPage(p);
            }

            outputDocument.Info.Author = "author name";

            string JSScript = string.Empty;
            JSScript += "var pp = this.getPrintParams(); ";

            if (NumCopies > 0)
            {
                JSScript += "pp.NumCopies = " + NumCopies.ToString() + "; ";
            }

            if (!ShowPrintDialog)
            {
                JSScript += "pp.interactive = pp.constants.interactionLevel.automatic; ";
            }


            JSScript += "this.print({printParams: pp}); ";


            PdfSharp.Pdf.PdfDictionary dictJS = new PdfSharp.Pdf.PdfDictionary();
            dictJS.Elements["/S"] = new PdfSharp.Pdf.PdfName("/JavaScript");
            dictJS.Elements["/JS"] = new PdfSharp.Pdf.PdfStringObject(JSScript, PdfSharp.Pdf.PdfStringEncoding.PDFDocEncoding);


            PdfSharp.Pdf.PdfDictionary dict = new PdfSharp.Pdf.PdfDictionary();
            PdfSharp.Pdf.PdfArray a = new PdfSharp.Pdf.PdfArray();
            dict.Elements["/Names"] = a;
            a.Elements.Add(new PdfSharp.Pdf.PdfString("EmbeddedJS"));
            a.Elements.Add(dictJS);

            
            //outputDocument.Internals.AddObject(dict);
            PdfSharp.Pdf.PdfPage p1 = outputDocument.Pages[0];
            var gfx = XGraphics.FromPdfPage(p1);
            var annot = new PdfLinkAnnotation();
            annot.Rectangle = new PdfRectangle(gfx.Transformer.WorldToDefaultPage(new XRect(new XPoint(100, 100), new XSize(600, 600))));
            annot.Elements["/A"] = dictJS;
            //annot.Elements.Add("/Names",dictJS);
            p1.Annotations.Add(annot);

            //PdfSharp.Pdf.PdfDictionary group = new PdfSharp.Pdf.PdfDictionary();
            //group.Elements["/JavaScript"] = PdfSharp.Pdf.Advanced.PdfInternals.GetReference(dict);

            //outputDocument.Internals.Catalog.Elements["/Names"] = group;


            //var rect = gfx.Transformer.WorldToDefaultPage(new XRect(new XPoint(100, 100), new XSize(600, 600)));
            //var linkAnnotation = PdfLinkAnnotation.CreateWebLink(new PdfRectangle(rect), "http://www.pdfsharp.net");
            //p1.Annotations.Add(linkAnnotation);


            MemoryStream ms = new MemoryStream();

            outputDocument.Save(ms, false);

            return ms;
        }
    }
}
