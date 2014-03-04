
namespace Skeptic.Model
{
    public class RuleViolation
    {
        public RuleViolation(string text)
        {
            this.Text = text;
        }

        public string Text { get; private set; }
    }
}
