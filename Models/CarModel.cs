namespace aspnet2.Models
{
    public class CarModel
    {
        public CarModel(List<Car> cars)
        {
            CarsEntity = cars;
        }

        public IEnumerable<Car> CarsEntity { get; }
    }
}
