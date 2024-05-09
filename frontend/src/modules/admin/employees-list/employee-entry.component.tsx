import { IEmployeeShortInfo } from './employees-list.component';
import TrashIcon from './../../../assets/icons/trash-icon.svg';
import { NavLink } from 'react-router-dom';

interface EmployeeEntryProps {
  employee: IEmployeeShortInfo;
}

const EmployeeEntry = ({ employee }: EmployeeEntryProps) => {
  // TODO:
  const onDeleteClick = () => {};

  return (
    <div className="my-2 flex items-center justify-between rounded-lg bg-accent-background p-3">
      <NavLink className="flex grow flex-col gap-3 md:flex-row" to={`/employees/${employee.id}`}>
        <span className="font-semibold md:basis-[40%]">
          {`${employee.lastName} ${employee.firstName.substring(0, 1)}. ${employee.patronymic.substring(0, 1)}.`}
        </span>
        <span className="md:basis-[35%]">{employee.email}</span>
        <span className="md:basis-[15%]">{employee.role}</span>
      </NavLink>
      <div className="flex items-center justify-between gap-1 md:gap-3">
        <button className="mr-1" onClick={onDeleteClick}>
          <img src={TrashIcon} />
        </button>
      </div>
    </div>
  );
};

export default EmployeeEntry;
