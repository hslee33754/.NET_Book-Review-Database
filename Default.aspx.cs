using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    BookReviewDbEntities db = new BookReviewDbEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) //keeps from reloading the list
        {
            var cats = from c in db.Categories
                       orderby c.CategoryName
                       select new { c.CategoryName, c.CategoryKey };

            DropDownList1.DataSource = cats.ToList();
            DropDownList1.DataTextField = "CategoryName";
            DropDownList1.DataValueField = "CategoryKey";
            DropDownList1.DataBind();
        }

    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string catName = DropDownList1.SelectedItem.ToString();
        //int catKey = int.Parse(DropDownList1.SelectedValue.ToString());
        
        var bks = from b in db.Books
                  from a in b.Authors
                  from c in b.Categories
                  orderby b.BookTitle
                  where c.CategoryName.Equals(catName)
                  //where c.CategoryKey == catKey
                  select new
                  {
                      b.BookTitle,
                      a.AuthorName,
                      b.BookISBN,
                      b.BookEntryDate

                  };
        GridView1.DataSource = bks.ToList();
        GridView1.DataBind();
    }
}