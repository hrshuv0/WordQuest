namespace Core.Entities;

public class WordQuest
{
    public long WordId { get; set; }
    public Word? Word { get; set; }

    public long WordOptionId { get; set; }
    public WordOption? WordOption { get; set; }
}