using FITApp.PublicationsService.Contracts.Responses;
using FITApp.PublicationsService.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FITApp.PublicationsService.Documents
{
    public class Report : IDocument
    {
        public Report(IEnumerable<FullPublication> publications, Author author, Administration administration)
        {
            Publications = publications;
            Author = author;
            Administration = administration;
        }

        private TextStyle TextStyle = TextStyle.Default.FontSize(12).FontFamily(Fonts.TimesNewRoman);
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
		public DocumentSettings GetSettings() => DocumentSettings.Default;

        public IEnumerable<FullPublication> Publications { get; }
        public Author Author { get; }
        public Administration Administration { get; }


        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.MarginTop(2, Unit.Centimetre);
                page.MarginBottom(1.5f, Unit.Centimetre);
                page.MarginHorizontal(1.5f, Unit.Centimetre);

                page.Content().Element(ComposeContent);
            });

            void ComposeContent(IContainer container)
            {
                container.Column(column =>
                {
                    column.Item().Element(ComposeHeader);



                    column.Item().Element(ComposeFooter);
                });
            }

            void ComposeHeader(IContainer container)
            {
                var headerStyle = TextStyle.SemiBold();
                container.Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text("СПИСОК").AlignCenter().Style(headerStyle);
                        column.Item().Text("наукових і навчально-методичних праць").AlignCenter().Style(headerStyle);

                        var degree = GetAcademicDegree();
                        if (degree != null)
                        {
                            column.Item().Text(GetAcademicDegree()).AlignCenter().Style(headerStyle);
                        }

                        // TODO change to right rank or profession
                        var rank = Author.AcademicRanks.OrderByDescending(ar => ar.DateOfIssue)
                        .ElementAtOrDefault(0)?.Name;
                        column.Item().Text($"{rank}а кафедри програмних систем і технологій факультету інформаційних технологій Київського національного університету імені Тараса Шевченка").AlignCenter().Style(headerStyle);

                        // TODO change to name in whose form
                        var FullName = $"{Author.FirstName.ToUpper()} {Author.LastName.ToUpper()} {Author.Patronymic.ToUpper()}";
                        column.Item().Text(FullName).AlignCenter().Style(headerStyle);
                    });


                });
            }

            void ComposeFooter(IContainer container)
            {
                throw new NotImplementedException();
            }
        }

        private string GetAcademicDegree()
        {
            var degrees = Author.AcademicDegrees.OrderByDescending(ad => ad.DateOfIssue);
            if (degrees.Any())
            {
                var words = degrees.ElementAt(0).FullName.Split();
                words[0] = words[0] + "а";
                var degree = string.Join(' ', words);
                return degree;
            }

            return null;
        }

    }
}