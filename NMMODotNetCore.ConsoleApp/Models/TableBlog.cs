using System;
using System.Collections.Generic;

namespace NMMODotNetCore.ConsoleApp.Models;

public partial class TableBlog
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string BlogContent { get; set; } = null!;
}
