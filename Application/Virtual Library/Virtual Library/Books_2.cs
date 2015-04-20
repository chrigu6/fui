using System;
using System.Collections.Generic;
using System.Drawing;

public class Books
{
    private String name;
    private String author;
    private int year;
    private String path;
    private Image thumbnail;
    private List<String> tags = new List<string>();

    public Books(String name, String author, int year, String pathToFile, String path)
    {
        this.name = name;
        this.author = author;
        this.year = year;
        this.thumbnail = Image.FromFile(pathToFile);
        this.path = path;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public String getName()
    {
        return this.name;
    }

    public void setAuthor(String author)
    {
        this.author = author;
    }

    public String getAuthor()
    {
        return this.author;
    }

    public void setYear(int year)
    {
        this.year = year;
    }

    public int getYear()
    {
        return this.year;
    }

    public void setPath(String path)
    {
        this.path = path;
    }

    public String getPath()
    {
        return this.path;
    }

    public void setThumbnail(String path)
    {
        this.thumbnail = Image.FromFile(path);
    }

    public Image getThumbnail()
    {
        return this.thumbnail;
    }

    public void setTag(String tag)
    {
        if (!this.tags.Contains(tag))
        {
            this.tags.Add(tag);
        }
    }

    public List<String> getTags()
    {
        return this.tags;
    }
}
