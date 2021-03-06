﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciadorDeImpressao
{
    static class PDFGenereator
    {
        private static PdfPTable pdfTable;

        public static void GenerateTable(DataGridView dgvR, string path)
        {

            //Creating iTextSharp Table from the DataTable data
            CreatingiTextSharpTable(dgvR);


            //Adding Header row
            AddingHeader(dgvR);

            //Adding DataRow
            AddingDataRow(dgvR);

            //Exporting to PDF
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
            
        }

        private static void AddingDataRow(DataGridView dgvR)
        {
            for (int i = 0; i < dgvR.Rows.Count - 1; i++)
            {
                foreach (DataGridViewCell cell in dgvR.Rows[i].Cells)
                {
                    pdfTable.AddCell(cell.Value.ToString());
                }
            }
        }

        private static double GetTotalCost(List<Print> prints)
        {
            double cost = 0.0;
            foreach (var item in prints)
            {
                cost += item.cost;
            }

            return cost;
        }

        private static int GetTotalPages(List<Print> prints)
        {
            int pages = 0;
            foreach (var item in prints)
            {
                pages += item.quantityPages;
            }

            return pages;
        }

        private static void CreatingiTextSharpTable(DataGridView dgvR)
        {
            pdfTable = new PdfPTable(dgvR.Columns.Count);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
        }

        private static void AddingHeader(DataGridView dgvR)
        {
            foreach (DataGridViewColumn column in dgvR.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }
        }
    }
}
