namespace App;

class Appointment
{
    public enum AppointmentStatus
    {
        Pending,
        Accepted,
        Denied,
    }
    public User Patient;
    public User Personell;
    public DateTime Date;
    public AppointmentStatus Status;

    public Appointment(User patient, User personell, DateTime date)
    {
        Patient = patient;
        Personell = personell;
        Date = date;
        Status = AppointmentStatus.Pending;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} with {Personell.Name} (Status: {Status})";
    }

}