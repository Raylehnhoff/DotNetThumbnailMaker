This is a neat small script that will allow you to link larger images and have C# scale them down for you.

The benefit here is, that you can link to this tool with the parameters and deliver a LARGE image in a thumbnail without having the client load the larger picture.

For example:

http://yoursite.com/makethumbnail.aspx?image=Image.png&width=500&height=500

Lets say Image.png is 2000x2000. Forcing a user to load the entire image and using CSS to resize that image would take a -very- long time. Instead, just link to the above file and they won't go through the long loading time!