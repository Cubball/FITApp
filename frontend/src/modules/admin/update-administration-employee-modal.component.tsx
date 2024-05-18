import { useState } from 'react';
import { IAdministrationEmployee } from '../../services/administration/administration.types';
import { IEmployeeShortInfo } from '../../services/employees/employees.types';
import Modal from '../../shared/components/modal';
import { Field, Form, Formik } from 'formik';

interface UpdateAdministrationEmployeeModalProps {
  isOpen: boolean;
  onClose: () => void;
  onUpdate: (employee: IAdministrationEmployee) => void;
  employee: IAdministrationEmployee;
  employees: IEmployeeShortInfo[];
}

const UpdateAdministrationEmployeeModal = ({
  isOpen,
  onClose,
  onUpdate,
  employee,
  employees
}: UpdateAdministrationEmployeeModalProps) => {
  const [isKnownEmployee, setIsKnownEmployee] = useState(!!employee.id);
  return (
    <Modal
      isOpen={isOpen}
      onClose={() => {
        setIsKnownEmployee(!!employee.id);
        onClose();
      }}
    >
      <Formik
        initialValues={{
          id: employee.id ?? '',
          firstName: employee.firstName,
          lastName: employee.lastName,
          patronymic: employee.patronymic
        }}
        onSubmit={(values) => {
          if (isKnownEmployee) {
            const employee = employees.find((e) => e.id === values.id);
            if (employee) {
              onUpdate(employee);
            }
          } else {
            values.id = undefined!;
            onUpdate(values);
          }

          setIsKnownEmployee(!!employee.id);
          onClose();
        }}
      >
        <Form className="gap-5 p-3 *:mb-1 sm:grid sm:grid-cols-2">
          <div className="col-span-2 flex items-center p-2">
            <input
              id="isKnownEmployee"
              name="isKnownEmployee"
              type="checkbox"
              className="h-5 w-5 rounded-md border border-gray-300 p-2"
              checked={isKnownEmployee}
              onChange={(e) => setIsKnownEmployee(e.target.checked)}
            />
            <label htmlFor="isKnownEmployee" className="ml-2 font-semibold">
              Співробітник зареєстрований у системі
            </label>
          </div>
          {isKnownEmployee ? (
            <Field
              name="id"
              as="select"
              className="col-span-2 w-full rounded-md border border-gray-300 p-2"
            >
              <option value="" disabled hidden>
                Оберіть співробітника
              </option>
              {employees.map((employee) => {
                let displayName = "<ім'я не вказано>";
                if (employee.firstName && employee.lastName && employee.patronymic) {
                  displayName = `${employee.lastName} ${employee.firstName} ${employee.patronymic}`;
                }
                return (
                  <option value={employee.id} key={employee.id}>
                    {displayName}
                  </option>
                );
              })}
            </Field>
          ) : (
            <>
              <div className="col-span-2">
                <label htmlFor="lastName" className="mb-1 ml-1 font-semibold">
                  Прізвище
                </label>
                <Field
                  name="lastName"
                  id="lastName"
                  required
                  className="w-full rounded-md border border-gray-300 p-2"
                />
              </div>
              <div>
                <label htmlFor="firstName" className="mb-1 ml-1 font-semibold">
                  Ім'я
                </label>
                <Field
                  name="firstName"
                  id="firstName"
                  required
                  className="w-full rounded-md border border-gray-300 p-2"
                />
              </div>
              <div>
                <label htmlFor="patronymic" className="mb-1 ml-1 font-semibold">
                  По-батькові
                </label>
                <Field
                  name="patronymic"
                  id="patronymic"
                  required
                  className="w-full rounded-md border border-gray-300 p-2"
                />
              </div>
            </>
          )}
          <button className="w-full rounded-md bg-main-text p-3 text-white">Зберегти</button>
          <button
            className="w-full rounded-md border border-main-text p-3"
            type="button"
            onClick={onClose}
          >
            Скасувати
          </button>
        </Form>
      </Formik>
    </Modal>
  );
};

export default UpdateAdministrationEmployeeModal;
