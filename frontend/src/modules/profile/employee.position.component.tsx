import { IEmployeePosition } from './types';

const EmployeePosition = ({ position }: { position: IEmployeePosition }) => {
  return (
    <div>
      <h3 className="text-md font-semibold">{position.name}</h3>
      <div className="italic">{`${position.startDate.toLocaleDateString('uk-UA')} - ${position.endDate.toLocaleDateString('uk-UA')}`}</div>
    </div>
  );
};

export default EmployeePosition;
