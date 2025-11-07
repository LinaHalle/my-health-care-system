namespace App;

class JournalEntry
{
    public User Patient;
    public User Author;
    public DateTime Date;
    public string Note;

    public JournalEntry(User patient, User author, string note)
    {
        Patient = patient;
        Author = author;
        Date = DateTime.Now;
        Note = note;
    }

    public override string ToString()
    {
        return $"{Date:g} - {Author.Name}: {Note}";
    }
}