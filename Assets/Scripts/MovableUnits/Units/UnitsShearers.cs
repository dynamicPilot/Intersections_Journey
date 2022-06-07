using System.Collections.Generic;

public class UnitsShearers
{
    private List<IPositionShearer> _positions;
    public List<IPositionShearer> Positions { get => _positions; }
    private List<IVelocityShearer> _velocities;
    public List<IVelocityShearer> Velocities { get => _velocities; }

    public UnitsShearers(List<IPositionShearer> positions, List<IVelocityShearer> velocities)
    {
        _positions = positions;
        _velocities = velocities;
    }
}

public class UnitShearer
{
    private IPositionShearer _position;
    public IPositionShearer Position { get => _position; }
    private IVelocityShearer _velocity;
    public IVelocityShearer Velocity { get => _velocity; }

    public UnitShearer(IPositionShearer position, IVelocityShearer velocity)
    {
        _position = position;
        _velocity = velocity;
    }
}
