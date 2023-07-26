namespace Core.Entities;

public class Word : BaseEntity<long>
{
    public string? Name { get; set; }
    public string? Definition { get; set; } // Meaning
    public string? PartOfSpeech { get; set; }
    public string? Pronunciation { get; set; }
    public string? Example { get; set; }
    public string? Translation { get; set; }

    public int DifficultyLevel { get; set; }
    public int CorrectAnswer { get; set; }
    public int WrongAnswer { get; set; }
}