import Modal from './modal';

interface ConfirmModalProps {
  isOpen: boolean;
  onClose: () => void;
  text: string;
  onConfirm: () => void;
}

const ConfirmModal = ({ isOpen, onClose, text, onConfirm }: ConfirmModalProps) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <h2 className="text-center text-lg font-semibold">Підтвердіть дію</h2>
      <p>{text}</p>
      <div className="flex justify-evenly gap-5 p-3 *:grow *:rounded-md *:p-1">
        <button
          onClick={() => {
            onConfirm();
            onClose();
          }}
          className="bg-main-text text-white"
        >
          Так
        </button>
        <button onClick={onClose} className="border border-main-text">
          Ні
        </button>
      </div>
    </Modal>
  );
};

export default ConfirmModal;
