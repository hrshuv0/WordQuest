namespace Core.Entities;

public class WordOption : BaseEntity<long>
{
    public string? Name { get; set; }
    public bool IsCorrect { get; set; }

    public List<WordQuest>? WordList { get; set; }
}