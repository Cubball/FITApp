import { IEmployeeData } from './types';
import EditIcon from '../../assets/icons/edit-icon.svg';

const EmployeeInfo = ({ employee }: { employee: IEmployeeData }) => {
  return (
    <div className="p-1">
      <div className="flex justify-between">
        <div className="border border-b-main-text border-l-white border-r-white border-t-white">
          <h1 className="relative left-10 top-1 text-lg font-semibold">Особисті дані</h1>
        </div>
        <button>
          <img src={EditIcon} className="inline" />
        </button>
      </div>
      <div className="flex flex-wrap py-5">
        <div className="flex grow basis-1/2">
          <div className="grow basis-1/6">
            <img
              src={employee.photoUrl}
              className="aspect-square rounded-full border-4 border-white object-cover shadow shadow-gray-400"
            />
          </div>
          <div className="flex grow basis-5/6 flex-col justify-evenly px-3">
            <div className="font-bold">{`${employee.firstName} ${employee.patronymic} ${employee.lastName}`}</div>
            <div className="font-medium">{employee.role}</div>
            <div>{employee.birthDate.toLocaleDateString('uk-UA')}</div>
          </div>
        </div>
        <div className="flex grow basis-1/2 items-center justify-end gap-8">
          <button className="h-fit grow rounded-lg bg-main-text px-1 py-3 text-center font-semibold text-white shadow shadow-gray-400 md:max-w-[40%]">
            Завантажити фото
          </button>
          <button className="h-fit grow rounded-lg border border-main-text px-1 py-3 text-center font-semibold shadow shadow-gray-400 md:max-w-[40%]">
            Видалити
          </button>
        </div>
      </div>
      <hr />
    </div>
  );
};

export default EmployeeInfo;
