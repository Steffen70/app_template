namespace API.Data
{
    public partial class UnitOfWork
    {
        public WeatherRepository WeatherRepository => new WeatherRepository(_context, this, _mapper);
    }
}