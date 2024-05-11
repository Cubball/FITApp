import { Formik, Form, Field } from 'formik';
import Modal from '../../modal';

interface AddAcademicRankModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: any;
}

const AddAcademicRankModal = ({ isOpen, onClose, onSubmit }: AddAcademicRankModalProps) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <h2 className="border-b border-gray-500 p-2 text-center text-lg font-semibold">
        Додати наукове звання
      </h2>
      <Formik
        initialValues={{
          name: '',
          certificateNumber: '',
          dateOfIssue: undefined
        }}
        onSubmit={(values) => {
          onSubmit(values);
          onClose();
        }}
      >
        <Form className="gap-5 p-3 *:mb-1 sm:grid sm:grid-cols-2">
          <div className="col-span-2">
            <label htmlFor="name" className="mb-1 ml-1 font-semibold">
              Назва
            </label>
            <Field
              id="name"
              name="name"
              required
              as="select"
              className="w-full rounded-md border border-gray-300 p-2"
            >
              <option value="Доцент">Доцент</option>
              <option value="Старший науковий співробітник">Старший науковий співробітник</option>
              <option value="Професор">Професор</option>
            </Field>
          </div>
          <div>
            <label htmlFor="certificateNumber" className="mb-1 ml-1 font-semibold">
              Номер сертифіката
            </label>
            <Field
              id="certificateNumber"
              name="certificateNumber"
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

export default AddAcademicRankModal;
