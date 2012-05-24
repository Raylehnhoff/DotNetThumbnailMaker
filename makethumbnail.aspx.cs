using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class makethumbnail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // get the file name
        string file = Request.QueryString["file"];
        string heightS = string.Empty;
        string widthS = string.Empty;
        if(Request.QueryString["width"] != null)
            widthS = Request.QueryString["width"].ToString();
        if(Request.QueryString["height"] != null)
            heightS = Request.QueryString["height"].ToString();
        System.Drawing.Image thumbnailImage;
        if(!string.IsNullOrEmpty(widthS) && !string.IsNullOrEmpty(heightS))
        {
            int width = int.Parse(Request.QueryString["width"].ToString());
            int height = int.Parse(Request.QueryString["height"].ToString());
            // create an image object, using the filename we just retrieved
            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(file.ToString()));

            // create the actual thumbnail image
            thumbnailImage = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
        }
        else
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(file.ToString()));
            // create the actual thumbnail image
            thumbnailImage = image.GetThumbnailImage(((int)(image.Width * .25)), ((int)(image.Height * .25)), new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
        }
        // make a memory stream to work with the image bytes
        MemoryStream imageStream = new MemoryStream();

        // put the image into the memory stream
        thumbnailImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        // make byte array the same size as the image
        byte[] imageContent = new Byte[imageStream.Length];

        // rewind the memory stream
        imageStream.Position = 0;

        // load the byte array with the image
        imageStream.Read(imageContent, 0, (int)imageStream.Length);

        // return byte array to caller with image type
        Response.ContentType = "image/jpeg";
        Response.BinaryWrite(imageContent);
    }

    /// <summary>
    /// Required, but not used
    /// </summary>
    /// <returns>true</returns>
    public bool ThumbnailCallback()
    {
        return true;
    }

}
