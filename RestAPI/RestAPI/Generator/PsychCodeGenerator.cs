namespace RestAPI.Generator
{
    public class PsychCodeGenerator
    {
        public static string GeneratePsychiatristCode()
        {
            return "PSYCH-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
        }
    }
}
