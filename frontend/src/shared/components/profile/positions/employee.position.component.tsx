import { IPosition } from '../../../../services/profile/profile.types';

interface EmployeePositionProps {
  position: IPosition;
}

const EmployeePosition = ({ position }: EmployeePositionProps) => {
  return (
    <div>
      <h3 className="text-md font-semibold">{position.name}</h3>
      <div className="italic">{`${new Date(position.startDate).toLocaleDateString('uk-UA')} - ${position.endDate ? new Date(position.endDate).toLocaleDateString('uk-UA') : 'до сьогодні'}`}</div>
    </div>
  );
};

export default EmployeePosition;
