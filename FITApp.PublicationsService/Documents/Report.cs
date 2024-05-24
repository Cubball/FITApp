using FITApp.PublicationsService.Contracts.Responses;
using FITApp.PublicationsService.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FITApp.PublicationsService.Documents
{
    public class Report : IDocument
    {
        public Report(
            IEnumerable<FullPublication> publications,
            Author author,
            Administration administration
        )
        {
            Publications = publications;
            Author = author;
            Administration = administration;
        }

        private TextStyle TextStyle = TextStyle
            .Default.FontSize(12)
            .FontFamily(Fonts.TimesNewRoman);

        private int worksCounter = 0;

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

                    column
                        .Item()
                        .PaddingVertical(4)
                        .Text(
                            "І. Наукові праці за профілем кафедри, опубліковані \nза звітний період (2023-2024 навчальний рік)"
                        )
                        .AlignCenter()
                        .SemiBold()
                        .Style(TextStyle);
                    ComposeTable(column.Item(), Publications);

                    column.Item().PaddingTop(20).Element(ComposeFooter);
                });
            }

            void ComposeHeader(IContainer container)
            {
                var headerStyle = TextStyle.SemiBold();
                container.Row(row =>
                {
                    row.RelativeItem()
                        .Column(column =>
                        {
                            column.Item().Text("СПИСОК").AlignCenter().Style(headerStyle);
                            column
                                .Item()
                                .Text("наукових і навчально-методичних праць")
                                .AlignCenter()
                                .Style(headerStyle);

                            var degree = GetAcademicDegree();
                            if (degree != null)
                            {
                                column
                                    .Item()
                                    .Text($"{degree.ToLower()},")
                                    .AlignCenter()
                                    .Style(headerStyle);
                            }

                            // TODO change to right rank or profession
                            var rank = Author
                                .AcademicRanks.OrderByDescending(ar => ar.DateOfIssue)
                                .ElementAtOrDefault(0)
                                ?.Name;
                            column
                                .Item()
                                .Text(
                                    $"{rank.ToLower()}а кафедри програмних систем і технологій факультету інформаційних технологій Київського національного університету імені Тараса Шевченка"
                                )
                                .AlignCenter()
                                .Style(headerStyle);

                            // TODO change to name in whose form
                            var fullName =
                                $"{Author.FirstName.ToUpper()} {Author.LastName.ToUpper()} {Author.Patronymic.ToUpper()}";
                            column.Item().Text($"{fullName}\n").AlignCenter().Style(headerStyle);
                        });
                });
            }

            void ComposeFooter(IContainer container)
            {
                container.Row(row =>
                {
                    row.RelativeItem(5)
                        .Column(column =>
                        {
                            column.Item().Text("Автор\n").AlignLeft().Style(TextStyle);
                            column.Item().Text("Список завіряю:\n").AlignLeft().Style(TextStyle);
                            column
                                .Item()
                                .Text("Завідувач кафедри _________________\n\n\n")
                                .AlignLeft()
                                .Style(TextStyle);
                            column
                                .Item()
                                .Text("Вчений секретар\nфакультету інформаційних технологій")
                                .AlignLeft()
                                .Style(TextStyle);
                        });

                    row.RelativeItem(2)
                        .Column(column =>
                        {
                            column
                                .Item()
                                .Text("_________________\n\n\n")
                                .AlignRight()
                                .Style(TextStyle);
                            column
                                .Item()
                                .Text("_________________\n\n\n")
                                .AlignRight()
                                .Style(TextStyle);
                            column.Item().Text("\n_________________").AlignRight().Style(TextStyle);
                        });

                    row.RelativeItem(3)
                        .Column(column =>
                        {
                            column
                                .Item()
                                .Text($"{Author.FirstName} {Author.LastName.ToUpper()}\n\n\n")
                                .AlignRight()
                                .Style(TextStyle);
                            column
                                .Item()
                                .Text(
                                    $"{Administration.HeadOfDepartment.FirstName} {Administration.HeadOfDepartment.LastName.ToUpper()}\n\n\n"
                                )
                                .AlignRight()
                                .Style(TextStyle);
                            column
                                .Item()
                                .Text(
                                    $"\n{Administration.ScientificSecretary.FirstName} {Administration.ScientificSecretary.LastName.ToUpper()}"
                                )
                                .AlignRight()
                                .Style(TextStyle);
                        });
                });
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

        private void ComposeTable(IContainer container, IEnumerable<FullPublication> publications)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(0.9f, Unit.Centimetre);
                    columns.RelativeColumn();
                    columns.RelativeColumn(0.4f);
                    columns.RelativeColumn();
                    columns.RelativeColumn(0.4f);
                    columns.RelativeColumn(0.8f);
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("№\nп/п");
                    header.Cell().Element(CellStyle).Text("Назва наукової праці");
                    header.Cell().Element(CellStyle).Text("Характер праці");
                    header.Cell().Element(CellStyle).Text("Вхідні дані");
                    header.Cell().Element(CellStyle).Text("Обсяг (у стор.)/ автор. доробок");
                    header.Cell().Element(CellStyle).Text("Співавтори");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .DefaultTextStyle(x =>
                                x.SemiBold().FontSize(13).FontFamily(Fonts.TimesNewRoman)
                            )
                            .Border(0.25f)
                            .BorderColor(Colors.Black);
                    }
                });

                foreach (var item in publications)
                {
                    worksCounter++;
                    table.Cell().Element(CellStyle).Text($"{worksCounter}");
                    table.Cell().Element(CellStyle).Text(item.Name);
                    table.Cell().Element(CellStyle).Text(item.Type);
                    table
                        .Cell()
                        .Element(CellStyle)
                        .Text($"{item.Annotation}\n\n{item.EVersionLink}");
                    table
                        .Cell()
                        .Element(CellStyle)
                        .Text($"{item.PagesCount}/{item.PagesByAuthorCount}");
                    table
                        .Cell()
                        .Element(CellStyle)
                        .Text(
                            string.Join(
                                ", ",
                                item.Authors.Select(ca =>
                                    $"{ca.LastName} {ca.FirstName[0]}.{ca.Patronymic[0]}."
                                )
                            )
                        );

                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .Border(0.25f)
                            .BorderColor(Colors.Black)
                            .Padding(2)
                            .DefaultTextStyle(x => x.FontSize(12).FontFamily(Fonts.TimesNewRoman));
                    }
                }
            });
        }
    }
}
