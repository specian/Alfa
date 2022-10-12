namespace Convertor;

public static class ApiMapping
{
    public static void Setup(WebApplication app, string apiPath, string fileRoot)
    {
        app.MapPost(apiPath + "/convert/{inFormat}/{outFormat}",
            async (ConvertService converter,
            string inFormat,
            string outFormat,
            string? inFile,
            string? outFile,
            string? inUri,
            string? outUri,
            HttpRequest request) =>
            {
                (bool success, string input) = await Helpers.GetInputTextAsync(inFile, inUri, request, fileRoot);
                if (!success)
                {
                    return Results.BadRequest(input);
                }

                string output = converter.Convert(inFormat, outFormat, input);
                if (await Helpers.OutputAsync(outFile, outUri, output, fileRoot))
                {
                    return Results.Ok(output);
                }

                return Results.BadRequest("Error: Unable to save to selected output.");
            })
           .WithName("ConvertDocument");
    }
}
