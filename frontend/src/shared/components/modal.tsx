import { PropsWithChildren } from 'react';

interface ModalProps extends PropsWithChildren {
  isOpen: boolean;
  onClose: () => void;
}

const Modal = ({ isOpen, onClose, children }: ModalProps) => {
  if (!isOpen) {
    return null;
  }

  return (
    <div
      onClick={onClose}
      className="fixed left-0 top-0 z-50 flex h-screen w-full items-center justify-center bg-black/30"
    >
      <div className="rounded-xl bg-white px-10 py-5" onClick={(e) => e.stopPropagation()}>
        {children}
      </div>
    </div>
  );
};

export default Modal;
