import { useLocation } from 'react-router-dom';
import Pagination from '../../../shared/components/pagination';
import EmployeeEntry from './employee-entry.component';
import { useEmployeesList } from '../../../hooks/employees/employees-list.hook';
import { IEmployeeShortInfo } from '../../../services/employees/employees.types';

const EmployeesList = () => {
  const location = useLocation();
  const page = new URLSearchParams(location.search).get('page');
  const { employeesList, isLoading, deleteEmployeeById } = useEmployeesList(Number(page ?? 1));
  // TODO:
  if (isLoading) return <h1>Loading...</h1>;
  if (!employeesList) return <h1>Error...</h1>;

  const totalPages = Math.ceil(employeesList.totalCount / employeesList.pageSize);
  console.log(totalPages);

  return (
    <div className="flex h-full flex-col items-center justify-between px-10 py-5">
      <div className="w-full">
        <h1 className="py-3 text-xl font-bold">Список користувачів</h1>
        {employeesList.employees.map((employee: IEmployeeShortInfo) => (
          <EmployeeEntry employee={employee} key={employee.id} onDelete={deleteEmployeeById} />
        ))}
      </div>
      <Pagination link="employees" totalPages={totalPages} page={employeesList.page} />
    </div>
  );
};

export default EmployeesList;
