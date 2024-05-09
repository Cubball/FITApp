import { ReactNode, useState } from 'react';
import TrashIcon from '../../../assets/icons/trash-icon.svg';
import ConfirmModal from '../confirm-modal';

interface ExpandableListItemProps {
  element: ReactNode;
  index: number;
  canExpand: boolean;
  canEdit: boolean;
  onDeleteClick: (index: number) => void;
}

const ExpandableListItem = ({
  element,
  index,
  canExpand,
  onDeleteClick,
  canEdit
}: ExpandableListItemProps) => {
  const [confirmModalOpen, setConfirmModalOpen] = useState(false);

  return (
    <>
      <ConfirmModal
        isOpen={confirmModalOpen}
        text="Ви впевнені що хочете видалити даний елемент?"
        onConfirm={() => onDeleteClick(index)}
        onClose={() => setConfirmModalOpen(false)}
      />
      <div className="flex items-center justify-between p-3">
        {element}
        {canEdit ? (
          <button onClick={() => setConfirmModalOpen(true)} className="min-w-fit">
            <img src={TrashIcon} />
          </button>
        ) : null}
      </div>
      {canExpand ? <hr /> : null}
    </>
  );
};

export default ExpandableListItem;
