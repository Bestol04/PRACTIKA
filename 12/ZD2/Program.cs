using System;

public interface IHttpRequest
{
    string GetHeaders();
}

public class BasicHttpRequest : IHttpRequest
{
    public string GetHeaders()
    {
        return "Base Request\n";
    }
}

public abstract class HttpRequestDecorator : IHttpRequest
{
    protected IHttpRequest request;

    protected HttpRequestDecorator(IHttpRequest request)
    {
        this.request = request;
    }

    public virtual string GetHeaders()
    {
        return request.GetHeaders();
    }
}

public class AuthHeaderDecorator : HttpRequestDecorator
{
    public AuthHeaderDecorator(IHttpRequest request)
        : base(request) { }

    public override string GetHeaders()
    {
        return base.GetHeaders() + "Authorization: Bearer TOKEN\n";
    }
}

public class ContentTypeDecorator : HttpRequestDecorator
{
    public ContentTypeDecorator(IHttpRequest request)
        : base(request) { }

    public override string GetHeaders()
    {
        return base.GetHeaders() + "Content-Type: application/json\n";
    }
}

public class CustomHeaderDecorator : HttpRequestDecorator
{
    private string header;

    public CustomHeaderDecorator(IHttpRequest request, string header)
        : base(request)
    {
        this.header = header;
    }

    public override string GetHeaders()
    {
        return base.GetHeaders() + $"{header}\n";
    }
}


public class Program
{
    public static void Main()
    {
        IHttpRequest request = new BasicHttpRequest();

        request = new AuthHeaderDecorator(request);
        request = new ContentTypeDecorator(request);
        request = new CustomHeaderDecorator(request, "X-Custom: MyHeader");

        Console.WriteLine(request.GetHeaders());
    }
}