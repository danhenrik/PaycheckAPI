namespace BreakEven.API.Exceptions;

public class NegativeSalaryException() : Exception("Salary cannot be less than 0");