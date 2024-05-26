import { useState } from 'react';
import Modal from '../../shared/components/modal';
import { Field, Form, Formik } from 'formik';
import { ICreateUpdatePublicationAuthor } from '../../services/publications/publications.types';
import { IEmployeeShortInfo } from '../../services/employees/employees.types';

interface AddAuthorModalProps {
  isOpen: boolean;
  onClose: () => void;
  onAdd: (author: ICreateUpdatePublicationAuthor) => void;
  employees: IEmployeeShortInfo[];
}

const AddAuthorModal = ({ isOpen, onAdd, onClose, employees }: AddAuthorModalProps) => {
  const [isKnownEmployee, setIsKnownEmployee] = useState(true);
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <Formik
        initialValues={{
          id: '',
          firstName: '',
          lastName: '',
          patronymic: '',
          pagesByAuthorCount: undefined,
        }}
        onSubmit={(values) => {
          if (isKnownEmployee) {
            const author = employees.find((e) => e.id === values.id);
            if (author) {
              onAdd({ ...author, pagesByAuthorCount: values.pagesByAuthorCount });
            }
          } else {
            values.id = undefined!;
            onAdd(values);
          }

          setIsKnownEmployee(true);
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
              Співавтор зареєстрований у системі
            </label>
          </div>
          {isKnownEmployee ? (
            <Field
              name="id"
              as="select"
              className="col-span-2 w-full rounded-md border border-gray-300 p-2"
            >
              <option value="" disabled hidden selected>
                Оберіть співавтора
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
          <div className="col-span-2">
            <label htmlFor="pagesByAuthorCount" className="mb-1 ml-1 font-semibold">
              Кількість сторінок, написаних автором (необов'язково)
            </label>
            <Field
              name="pagesByAuthorCount"
              id="pagesByAuthorCount"
              className="w-full rounded-md border border-gray-300 p-2"
              type="number"
              min="1"
            />
          </div>
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

export default AddAuthorModal;
