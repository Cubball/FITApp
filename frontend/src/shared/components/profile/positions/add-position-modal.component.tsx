import { Formik, Form, Field } from 'formik';
import Modal from '../../modal';
import { useState } from 'react';

interface AddPositionModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: any;
}

const AddPositionModal = ({ isOpen, onClose, onSubmit }: AddPositionModalProps) => {
  const [isCurrentPosition, setIsCurrentPosition] = useState(false);
  const onModalClose = () => {
    setIsCurrentPosition(false)
    onClose();
  }

  return (
    <Modal isOpen={isOpen} onClose={onModalClose}>
      <h2 className="border-b border-gray-500 p-2 text-center text-lg font-semibold">
        Додати посаду
      </h2>
      <Formik
        initialValues={{
          name: 'Асистент',
          startDate: undefined,
          endDate: undefined
        }}
        onSubmit={(values) => {
          if (isCurrentPosition) {
            values.endDate = undefined
          }

          onSubmit(values);
          onModalClose();
        }}
      >
        <Form className="gap-5 p-3 *:mb-1 sm:grid sm:grid-cols-2">
          <div className="flex items-center col-span-2 p-2">
            <input
              id="isCurrentPosition"
              name="isCurrentPosition"
              type="checkbox"
              className="h-5 w-5 rounded-md border border-gray-300 p-2"
              checked={isCurrentPosition}
              onChange={(e) => setIsCurrentPosition(e.target.checked)}
            />
            <label htmlFor="isCurrentPosition" className="ml-2 font-semibold">
              Я зараз обіймаю цю посаду
            </label>
          </div>
          <div>
            <label htmlFor="startDate" className="mb-1 ml-1 font-semibold">
              Дата початку
            </label>
            <Field
              id="startDate"
              name="startDate"
              type="date"
              required
              className="w-full rounded-md border border-gray-300 p-2"
            />
          </div>
          <div>
            <label htmlFor="endDate" className="mb-1 ml-1 font-semibold">
              Дата закінчення
            </label>
            <Field
              id="endDate"
              name="endDate"
              type="date"
              required={!isCurrentPosition}
              disabled={isCurrentPosition}
              className="w-full rounded-md border border-gray-300 p-2 disabled:border-main-text disabled:bg-gray-300"
            />
          </div>
          <div>
            <label htmlFor="name" className="mb-1 ml-1 font-semibold">
              Посада
            </label>
            <Field
              id="name"
              name="name"
              required
              as="select"
              className="w-full rounded-md border border-gray-300 p-2"
            >
              <option value="Асистент">Асистент</option>
              <option value="Доцент">Доцент</option>
              <option value="Професор">Професор</option>
            </Field>
          </div>
          <div className='col-span-2'></div>
          <button className="w-full rounded-md bg-main-text p-3 text-white">Зберегти</button>
          <button
            className="w-full rounded-md border border-main-text p-3"
            type="button"
            onClick={onModalClose}
          >
            Скасувати
          </button>
        </Form>
      </Formik>
    </Modal>
  );
};

export default AddPositionModal;
