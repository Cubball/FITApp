import { useState } from 'react';
import { IPublicationShortInfo } from '../../services/publications/publications.types';
import ConfirmModal from '../../shared/components/confirm-modal';
import { NavLink } from 'react-router-dom';
import TrashIcon from './../../assets/icons/trash-icon.svg';

interface PublicationEntryProps {
  publication: IPublicationShortInfo;
  onDelete: (id: string) => void;
}

const PublicationEntry = ({ publication, onDelete }: PublicationEntryProps) => {
  const [confirmDeleteModalOpen, setConfirmDeleteModalOpen] = useState(false);
  return (
    <>
      <ConfirmModal
        isOpen={confirmDeleteModalOpen}
        text={`Ви впевнені що хочете видалити пубілкацію "${publication.name}"?`}
        onConfirm={() => onDelete(publication.id)}
        onClose={() => setConfirmDeleteModalOpen(false)}
      />
      <div className="my-2 flex items-center justify-between rounded-lg bg-accent-background p-3">
        <div className="flex grow flex-col gap-3 md:flex-row">
          <NavLink className="font-semibold md:basis-[40%]" to={`/publications/${publication.id}`}>
            {publication.name}
          </NavLink>
          <span className="md:basis-[35%]">{publication.type}</span>
          <span className="md:basis-[15%]">
            {new Date(publication.dateOfPublication).toLocaleDateString('uk-UA')}
          </span>
        </div>
        <div className="flex items-center justify-between gap-1 md:gap-3">
          <button className="mr-1" onClick={() => setConfirmDeleteModalOpen(true)}>
            <img src={TrashIcon} />
          </button>
        </div>
      </div>
    </>
  );
};

export default PublicationEntry;
