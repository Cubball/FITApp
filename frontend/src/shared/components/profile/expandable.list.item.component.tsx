import TrashIcon from '../../../assets/icons/trash-icon.svg';

const ExpandableListItem = ({ element, index, canExpand, onDeleteClick, canEdit }) => {
  return (
    <>
      <div className="flex items-center justify-between p-3">
        {element}
        {canEdit ? (
          <button onClick={() => onDeleteClick(index)} className="min-w-fit">
            <img src={TrashIcon} />
          </button>
        ) : null}
      </div>
      {canExpand ? <hr /> : null}
    </>
  );
};

export default ExpandableListItem;
