import { useLocation } from 'react-router-dom';
import Pagination from '../../../shared/components/pagination';
import EmployeeEntry from './employee-entry.component';
import { IEmployeeShortInfo } from '../../../services/employees/employees.types';
import { useEmployeesList } from '../../../shared/hooks/employees-list.hook';
import Loading from '../../../shared/components/loading';
import Error from '../../../shared/components/error';

const EmployeesList = () => {
  const location = useLocation();
  const page = new URLSearchParams(location.search).get('page');
  const { employeesList, isLoading, deleteEmployeeById, resetPasswordById } = useEmployeesList(
    Number(page ?? 1)
  );
  if (isLoading) {
    return <Loading />;
  }

  if (!employeesList) {
    return <Error />;
  }

  const totalPages = Math.ceil(employeesList.totalCount / employeesList.pageSize);
  return (
    <div className="flex h-full flex-col items-center justify-between px-10 py-5">
      <div className="w-full">
        <h1 className="py-3 text-xl font-bold">Список користувачів</h1>
        {employeesList.employees.map((employee: IEmployeeShortInfo) => (
          <EmployeeEntry
            employee={employee}
            key={employee.id}
            onDelete={deleteEmployeeById}
            onPasswordReset={resetPasswordById}
          />
        ))}
      </div>
      <Pagination link="employees" totalPages={totalPages} page={employeesList.page} />
    </div>
  );
};

export default EmployeesList;
