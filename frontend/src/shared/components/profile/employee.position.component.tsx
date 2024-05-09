import { IPosition } from "../../../services/profile/profile.types";

const EmployeePosition = ({ position }: { position: IPosition }) => {
  return (
    <div>
      <h3 className="text-md font-semibold">{position.name}</h3>
      <div className="italic">{`${position.startDate.toLocaleDateString('uk-UA')} - ${position.endDate?.toLocaleDateString('uk-UA') ?? 'до сьогодні'}`}</div>
    </div>
  );
};

export default EmployeePosition;
