import { IEmployee, IUpdateEmployeeBody } from '../../../../services/profile/profile.types';
import EditIcon from '../../../../assets/icons/edit-icon.svg';
import EditInfoModal from './edit-info-modal.component';
import { useState } from 'react';
import ConfirmModal from '../../confirm-modal';

interface EmployeeInfoProps {
  employee: IEmployee;
  canEdit: boolean;
  onSumbit: (body: IUpdateEmployeeBody) => void;
  onPhotoUpload: (photo: File) => void;
  onPhotoDelete: () => void;
}

const EmployeeInfo = ({
  employee,
  canEdit,
  onSumbit,
  onPhotoUpload,
  onPhotoDelete
}: EmployeeInfoProps) => {
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [confirmModalOpen, setConfirmModalOpen] = useState(false);
  let displayName = "<ім'я не вказано>";
  if (employee.firstName && employee.lastName && employee.patronymic) {
    displayName = `${employee.firstName} ${employee.patronymic} ${employee.lastName}`;
  }

  return (
    <>
      <EditInfoModal
        employee={employee}
        isOpen={editModalOpen}
        onSubmit={onSumbit}
        onClose={() => setEditModalOpen(false)}
      />
      <ConfirmModal
        isOpen={confirmModalOpen}
        onClose={() => setConfirmModalOpen(false)}
        text="Ви впевнені, що хочете видалити фото?"
        onConfirm={onPhotoDelete}
      />
      <div className="p-1">
        <div className="flex justify-between">
          <div className="border border-b-main-text border-l-white border-r-white border-t-white">
            <h1 className="relative left-10 top-1 text-lg font-semibold">Особисті дані</h1>
          </div>
          {canEdit ? (
            <button onClick={() => setEditModalOpen(true)}>
              <img src={EditIcon} className="inline" />
            </button>
          ) : null}
        </div>
        <div className="flex flex-wrap gap-5 py-5 md:gap-0">
          <div className="flex grow basis-1/2">
            <div className="grow basis-1/6">
              {employee.photo ? (
                <img
                  src={employee.photo}
                  className="aspect-square rounded-full border-4 border-white object-cover shadow shadow-gray-400"
                />
              ) : (
                <div className="w-full text-center italic">Фото відсутнє</div>
              )}
            </div>
            <div className="flex grow basis-5/6 flex-col justify-evenly px-3">
              <div className="font-bold">{displayName}</div>
              <div className="font-medium">{employee.user.role}</div>
              <div>
                {employee.birthDate
                  ? new Date(employee.birthDate).toLocaleDateString('uk-UA')
                  : '<дату народження не вказано>'}
              </div>
            </div>
          </div>
          <div className="flex grow basis-1/2 items-center justify-end gap-8">
            {canEdit ? (
              <>
                <label
                  htmlFor="upload-photo"
                  className="h-fit grow cursor-pointer rounded-lg bg-main-text px-1 py-3 text-center font-semibold text-white shadow shadow-gray-400 md:max-w-[40%]"
                >
                  Завантажити фото
                </label>
                <input
                  type="file"
                  className="hidden"
                  id="upload-photo"
                  accept="image/png, image/jpeg"
                  onChange={(e) => {
                    if (e.target.files && e.target.files[0]) {
                      onPhotoUpload(e.target.files[0]);
                    }
                  }}
                />
                <button
                  className="h-fit grow rounded-lg border border-main-text px-1 py-3 text-center font-semibold shadow shadow-gray-400 md:max-w-[40%]"
                  onClick={() => setConfirmModalOpen(true)}
                >
                  Видалити
                </button>
              </>
            ) : null}
          </div>
        </div>
        <hr />
      </div>
    </>
  );
};

export default EmployeeInfo;
