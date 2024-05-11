import { Formik, Form, Field } from 'formik';
import Modal from '../../modal';

interface AddAcademicDegreeModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: any;
}

const AddAcademicDegreeModal = ({ isOpen, onClose, onSubmit }: AddAcademicDegreeModalProps) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <h2 className="border-b border-gray-500 p-2 text-center text-lg font-semibold">
        Додати науковий ступінь
      </h2>
      <Formik
        initialValues={{
          fullName: '',
          shortName: '',
          diplomaNumber: '',
          dateOfIssue: undefined
        }}
        onSubmit={(values) => {
          onSubmit(values);
          onClose();
        }}
      >
        <Form className="gap-5 p-3 *:mb-1 sm:grid sm:grid-cols-2">
          <div>
            <label htmlFor="fullName" className="mb-1 ml-1 font-semibold">
              Повна назва
            </label>
            <Field
              id="fullName"
              name="fullName"
              required
              className="w-full rounded-md border border-gray-300 p-2"
            />
          </div>
          <div>
            <label htmlFor="shortName" className="mb-1 ml-1 font-semibold">
              Коротка назва
            </label>
            <Field
              id="shortName"
              name="shortName"
              required
              className="w-full rounded-md border border-gray-300 p-2"
            />
          </div>
          <div>
            <label htmlFor="diplomaNumber" className="mb-1 ml-1 font-semibold">
              Номер диплому
            </label>
            <Field
              id="diplomaNumber"
              name="diplomaNumber"
              required
              className="w-full rounded-md border border-gray-300 p-2"
            />
          </div>
          <div>
            <label htmlFor="dateOfIssue" className="mb-1 ml-1 font-semibold">
              Дата видачі
            </label>
            <Field
              id="dateOfIssue"
              name="dateOfIssue"
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

export default AddAcademicDegreeModal;
