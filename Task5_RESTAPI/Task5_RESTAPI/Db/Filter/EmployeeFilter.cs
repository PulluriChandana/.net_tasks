namespace Task5_RESTAPI.Db.Filter
{
    public class EmployeeFilter
    {
        public int? DepartmentId { get; set; }
        public Gender? Gender { get; set; }
        public int? SalaryGreaterThan { get; set; }
        public int? SalaryLessThan { get; set; }

    }
}
