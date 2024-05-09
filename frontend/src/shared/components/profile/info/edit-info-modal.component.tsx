import { Formik, Form, Field } from 'formik';
import { IEmployee } from '../../../../services/profile/profile.types';
import Modal from '../../modal';

interface EditInfoModalProps {
  employee: IEmployee;
  isOpen: boolean;
  onClose: () => void;
  onSubmit: any;
}

const EditInfoModal = ({ employee, isOpen, onClose, onSubmit }: EditInfoModalProps) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <h2 className="border-b border-gray-500 p-2 text-center text-lg font-semibold">
        Особисті дані
      </h2>
      <Formik
        initialValues={{
          firstName: employee.firstName,
          lastName: employee.lastName,
          patronymic: employee.patronymic,
          birthDate: employee.birthDate
        }}
        onSubmit={(values) => {
          onSubmit(values);
          onClose();
        }}
      >
        <Form className="gap-5 p-3 *:mb-1 sm:grid sm:grid-cols-2">
          <div>
            <label htmlFor="firstName" className="mb-1 ml-1 font-semibold">
              Ім'я
            </label>
            <Field
              id="firstName"
              name="firstName"
              required
              className="w-full rounded-md border border-gray-300 p-2"
            />
          </div>
          <div>
            <label htmlFor="lastName" className="mb-1 ml-1 font-semibold">
              Прізвище
            </label>
            <Field
              id="lastName"
              name="lastName"
              required
              className="w-full rounded-md border border-gray-300 p-2"
            />
          </div>
          <div>
            <label htmlFor="patronymic" className="mb-1 ml-1 font-semibold">
              По-батькові
            </label>
            <Field
              id="patronymic"
              name="patronymic"
              required
              className="w-full rounded-md border border-gray-300 p-2"
            />
          </div>
          <div>
            <label htmlFor="birthDate" className="mb-1 ml-1 font-semibold">
              Дата народження
            </label>
            <Field
              id="birthDate"
              name="birthDate"
              type="date"
              required
              className="w-full rounded-md border border-gray-300 p-2"
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

export default EditInfoModal;
