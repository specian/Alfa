namespace Convertor;

public static class Helpers
{
    public static async Task<(bool success, string input)> GetInputTextAsync(string? inFile, string? inUri, HttpRequest request, string fileRoot)
    {
        string input;
        if (inFile is not null)
        {
            string p = Path.Combine(fileRoot, inFile);
            string path = Path.GetFullPath(p);
            if (!path.StartsWith(fileRoot))
            {
                return (false, "Error: Wrong path.");
            }

            try
            {
                input = await File.ReadAllTextAsync(path);
                return (true, input);
            }
            catch (Exception ex)
            {
                input = $"Error: {ex.Message}";
                return (false, input);
            }
        }

        if (inUri is not null)
        {
            input = ""; //TODO
            return (true, input);
        }

        input = await new StreamReader(request.Body).ReadToEndAsync();
        return (true, input);
    }

    public static async Task MailFormatAndSendAsync(string mailBody, string eMailAddress)
    {
        // TODO
    }

    public static async Task<bool> OutputAsync(string? outFile, string? outUri, string output, string fileRoot)
    {
        if (outFile is not null)
        {
            string p = Path.Combine(fileRoot, outFile);
            string path = Path.GetFullPath(p);
            if (!path.StartsWith(fileRoot))
                return false;

            try
            {
                await File.WriteAllTextAsync(path, output);
            }
            catch
            {
                return false;
            }
        }

        if (outUri is not null)
        {
            if (outUri.StartsWith("mailto:"))
            {

                try
                {
                    await MailFormatAndSendAsync(output, outUri[7..]);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            // Obsloužení jiných protokolů
            //if (outUri.StartsWith())
            //{ 
            //}
        }

        return true;
    }
}
