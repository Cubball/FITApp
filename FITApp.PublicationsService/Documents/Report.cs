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
            IEnumerable<Publication> publications,
            Author author,
            Administration administration,
            DateOnly startDate,
            DateOnly endDate
        )
        {
            Publications = publications;
            Author = author;
            Administration = administration;
            StartDate = startDate;
            EndDate = endDate;
        }

        private TextStyle TextStyle = TextStyle
            .Default.FontSize(12)
            .FontFamily(Fonts.TimesNewRoman);

        private int worksCounter = 0;

        private int sectionsCounter = 0;

        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public IEnumerable<Publication> Publications { get; }
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
                    var lastDegree = Author
                        .AcademicDegrees.OrderByDescending(a => a.DateOfIssue)
                        .ElementAtOrDefault(0);
                    if (
                        lastDegree is not null
                        && lastDegree.DateOfIssue > StartDate
                        && lastDegree.DateOfIssue < EndDate
                    )
                    {
                        var degreeName = lastDegree.FullName.Split()[0].ToLower();

                        var publicationsBeforeDegree = Publications
                            .Where(p =>
                                p.Type != "Методичні вказівки"
                                && p.Type != "Авторське право"
                                && p.DateOfPublication < lastDegree.DateOfIssue
                            )
                            .ToList();

                        var publicationsAfterDegree = Publications
                            .Where(p =>
                                p.Type != "Методичні вказівки"
                                && p.Type != "Авторське право"
                                && p.DateOfPublication > lastDegree.DateOfIssue
                            )
                            .ToList();

                        if (publicationsBeforeDegree.Count > 0)
                        {
                            sectionsCounter++;
                            column
                                .Item()
                                .PaddingVertical(4)
                                .Text(
                                    $"{ToRomanNumeral(sectionsCounter)}. Наукові праці за профілем кафедри, опубліковані \nдо захисту {degreeName}ської дисертації"
                                )
                                .AlignCenter()
                                .SemiBold()
                                .Style(TextStyle);
                            ComposeTable(column.Item(), publicationsBeforeDegree);
                        }

                        if (publicationsAfterDegree.Count > 0)
                        {
                            sectionsCounter++;
                            column
                                .Item()
                                .PaddingVertical(4)
                                .Text(
                                    $"{ToRomanNumeral(sectionsCounter)}. Наукові праці за профілем кафедри, опубліковані \nпісля захисту {degreeName}ської дисертації"
                                )
                                .AlignCenter()
                                .SemiBold()
                                .Style(TextStyle);
                            ComposeTable(column.Item(), publicationsAfterDegree);
                        }
                    }
                    else
                    {
                        var works = Publications
                            .Where(p =>
                                p.Type != "Методичні вказівки" && p.Type != "Авторське право"
                            )
                            .ToList();

                        if (works.Count > 0)
                        {
                            sectionsCounter++;
                            column
                                .Item()
                                .PaddingVertical(4)
                                .Text(
                                    $"{ToRomanNumeral(sectionsCounter)}. Наукові праці за профілем кафедри, опубліковані \nза звітний період"
                                )
                                .AlignCenter()
                                .SemiBold()
                                .Style(TextStyle);
                            ComposeTable(column.Item(), works);
                        }
                    }

                    var copyrightWorks = Publications
                        .Where(p => p.Type == "Авторське право")
                        .ToList();

                    if (copyrightWorks.Count > 0)
                    {
                        copyrightWorks.ForEach(p => p.Type = string.Empty);
                        sectionsCounter++;
                        column
                            .Item()
                            .PaddingVertical(4)
                            .Text(
                                $"{ToRomanNumeral(sectionsCounter)}. Свідоцтво про реєстрацію авторського права за звітний період"
                            )
                            .AlignCenter()
                            .SemiBold()
                            .Style(TextStyle);
                        ComposeTable(column.Item(), copyrightWorks);
                    }

                    var methodicalInstructions = Publications
                        .Where(p => p.Type == "Методичні вказівки")
                        .ToList();

                    if (methodicalInstructions.Count > 0)
                    {
                        sectionsCounter++;
                        column
                            .Item()
                            .PaddingVertical(4)
                            .Text(
                                $"{ToRomanNumeral(sectionsCounter)}. Основні навчально-методичні роботи за звітний період"
                            )
                            .AlignCenter()
                            .SemiBold()
                            .Style(TextStyle);
                        ComposeTable(column.Item(), methodicalInstructions);
                    }

                    column.Item().Element(ComposeFooter);
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

                            column
                                .Item()
                                .Text($"за період - {StartDate.Year} - {EndDate.Year} роки")
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

                            var position = Author
                                .Positions.OrderByDescending(ar => ar.StartDate)
                                .ElementAtOrDefault(0)
                                ?.Name;
                            column
                                .Item()
                                .Text(
                                    $"{position?.ToLower()}а кафедри програмних систем і технологій факультету інформаційних технологій Київського національного університету імені Тараса Шевченка"
                                )
                                .AlignCenter()
                                .Style(headerStyle);

                            // TODO change to name in possessive form
                            var fullName =
                                $"{Author.FirstNamePossessive?.ToUpper()} {Author.LastNamePossessive?.ToUpper()} {Author.PatronymicPossessive?.ToUpper()}";
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
                                .Text("Завідувач кафедри\nпрограмних систем і технологій\n\n\n")
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
                                .Text("_________________\n\n\n\n")
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
                                .Text($"{Author.FirstName} {Author.LastName?.ToUpper()}\n\n\n\n")
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

        private void ComposeTable(IContainer container, IEnumerable<Publication> publications)
        {
            container
                .PaddingBottom(20)
                .Table(table =>
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
                            .Text($"{item.InputData}");
                        table
                            .Cell()
                            .Element(CellStyle)
                            .Text(
                                $"{item.PagesTotal}/{item.Authors.First(a => a.Id == Author.Id).PagesByAuthor}"
                            );
                        table
                            .Cell()
                            .Element(CellStyle)
                            .Text(
                                string.Join(
                                    ", ",
                                    item.Authors.Where(a => a.Id != Author.Id)
                                        .Select(ca =>
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
                                .DefaultTextStyle(x =>
                                    x.FontSize(12).FontFamily(Fonts.TimesNewRoman)
                                );
                        }
                    }
                });
        }

        private static List<string> romanNumerals = new List<string>()
        {
            "M",
            "CM",
            "D",
            "CD",
            "C",
            "XC",
            "L",
            "XL",
            "X",
            "IX",
            "V",
            "IV",
            "I"
        };
        private static List<int> numerals = new List<int>()
        {
            1000,
            900,
            500,
            400,
            100,
            90,
            50,
            40,
            10,
            9,
            5,
            4,
            1
        };

        private static string ToRomanNumeral(int number)
        {
            var romanNumeral = string.Empty;
            while (number > 0)
            {
                // find biggest numeral that is less than equal to number
                var index = numerals.FindIndex(x => x <= number);
                // subtract it's value from your number
                number -= numerals[index];
                // tack it onto the end of your roman numeral
                romanNumeral += romanNumerals[index];
            }
            return romanNumeral;
        }
    }
}
