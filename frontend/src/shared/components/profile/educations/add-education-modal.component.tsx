import { Formik, Form, Field } from 'formik';
import Modal from '../../modal';

interface AddEducationModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: any;
}

const AddEducationModal = ({ isOpen, onClose, onSubmit }: AddEducationModalProps) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <h2 className="border-b border-gray-500 p-2 text-center text-lg font-semibold">
        Додати освіту
      </h2>
      <Formik
        initialValues={{
          university: '',
          specialty: '',
          diplomaDateOfIssue: undefined
        }}
        onSubmit={(values) => {
          onSubmit(values);
          onClose();
        }}
      >
        <Form className="gap-5 p-3 *:mb-1 sm:grid sm:grid-cols-2">
          <div className="col-span-2">
            <label htmlFor="university" className="mb-1 ml-1 font-semibold">
              Навчальний заклад
            </label>
            <Field
              id="university"
              name="university"
              required
              className="w-full rounded-md border border-gray-300 p-2"
            />
          </div>
          <div>
            <label htmlFor="specialty" className="mb-1 ml-1 font-semibold">
              Спеціальність
            </label>
            <Field
              id="specialty"
              name="specialty"
              required
              className="w-full rounded-md border border-gray-300 p-2"
            />
          </div>
          <div>
            <label htmlFor="diplomaDateOfIssue" className="mb-1 ml-1 font-semibold">
              Дата видачі диплому
            </label>
            <Field
              id="diplomaDateOfIssue"
              name="diplomaDateOfIssue"
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

export default AddEducationModal;
