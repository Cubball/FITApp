import { useLocation } from 'react-router-dom';
import Pagination from '../../../shared/components/pagination';
import EmployeeEntry from './employee-entry.component';

// TODO: move somewhere appropriate
export interface IPagedEmployeesList {
  page: number;
  pageSize: number;
  total: number;
  employees: IEmployeeShortInfo[];
}

export interface IEmployeeShortInfo {
  id: number;
  firstName: string;
  lastName: string;
  patronymic: string;
  email: string;
  role: string;
}

const list: IPagedEmployeesList = {
  page: 1,
  pageSize: 10,
  total: 3,
  employees: [
    {
      email: 'some@longer.email',
      firstName: 'Святослав',
      lastName: 'Довгепрізвищедуже',
      patronymic: 'Миколайович',
      id: 1,
      role: 'Викладач'
    },
    {
      email: 'foo@bar.baz',
      firstName: 'Іван',
      lastName: 'Острозький',
      patronymic: 'Миколайович',
      id: 2,
      role: 'Викладач'
    },
    {
      email: 'foo@bar.baz',
      firstName: 'Іван',
      lastName: 'Острозький',
      patronymic: 'Миколайович',
      id: 3,
      role: 'Викладач'
    }
  ]
};

const EmployeesList = () => {
  // TODO: fetch actual users
  const location = useLocation();
  const page = new URLSearchParams(location.search).get('page');
  const totalPages = Math.ceil(list.total / list.pageSize)

  return (
    <div className="flex h-full flex-col justify-between px-10 py-5 items-center">
      <div className='w-full'>
        <h1 className="text-xl font-bold py-3">Список користувачів</h1>
        {list.employees.map((employee) => (
          <EmployeeEntry employee={employee} key={employee.id} />
        ))}
      </div>
      <Pagination link='' totalPages={totalPages} page={Number(page)}/>
    </div>
  );
};

export default EmployeesList;
