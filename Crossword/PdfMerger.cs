﻿using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Crossword;

/// <summary>
///     This class provides PDF Utility functions.
/// </summary>
public class PdfMerger : IPdfMerger
{
    private readonly IConsole _console;

    public PdfMerger(IConsole console)

    {
        _console = console;
    }


    /// <summary>
    ///     Merge a selection of source PDF files into a single PDF.
    /// </summary>
    /// <param name="output">The path of the file to create</param>
    /// <param name="sourceFiles">An array of the paths of the files to merge together.</param>
    public void Merge(string output, params string[] sourceFiles)
    {
        _console.WriteLine("Writing to single pdf");
        using var outPdf = new PdfDocument();
        if (sourceFiles.Any())
        {
            foreach (var file in sourceFiles)
            {
                using var pdfFile = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                CopyPages(pdfFile, outPdf);
            }

            outPdf.Save(output);
        }
    }

    /// <summary>
    ///     Copy an individual file from one document to another
    /// </summary>
    /// <param name="from">The source PDF document</param>
    /// <param name="to">The destination PDF document</param>
    private static void CopyPages(PdfDocument from, PdfDocument to)
    {
        for (var i = 0; i < from.PageCount; i++)
        {
            to.AddPage(from.Pages[i]);
        }
    }
}