DragDrop = {
    _draggedCardId: null,
    _draggedSourceColumnId: null,
    _draggedColumnId: null,
    _lastY: 0,
    _lastX: 0,

    init: function () {
        document.addEventListener('dragover', function (e) {
            if (DragDrop._draggedColumnId !== null) {
                const row = e.target.closest('.board-columns-row');
                if (row) {
                    e.preventDefault();
                    DragDrop.handleColumnDragOver(row, e.clientX);
                    return;
                }
            }
            const column = e.target.closest('.board-column');
            if (!column) return;
            e.preventDefault();
            const columnId = column.getAttribute('data-column-id');
            if (columnId) DragDrop.handleCardDragOver(parseInt(columnId), e.clientY);
        });

        document.addEventListener('drop', function (e) {
            if (DragDrop._draggedColumnId !== null) {
                const row = e.target.closest('.board-columns-row');
                if (row) { e.preventDefault(); return; }
            }
            const column = e.target.closest('.board-column');
            if (column) e.preventDefault();
        });
    },

    setDragData: function (cardId, sourceColumnId) {
        DragDrop._draggedCardId = cardId;
        DragDrop._draggedSourceColumnId = sourceColumnId;
        DragDrop._draggedColumnId = null;
    },

    setColumnDragData: function (columnId) {
        DragDrop._draggedColumnId = columnId;
        DragDrop._draggedCardId = null;
        DragDrop._draggedSourceColumnId = null;
    },

    getDraggedCardId: function () {
        return DragDrop._draggedCardId;
    },

    getDraggedSourceColumnId: function () {
        return DragDrop._draggedSourceColumnId;
    },

    getDraggedColumnId: function () {
        return DragDrop._draggedColumnId;
    },

    getDropIndex: function (columnId) {
        const column = document.querySelector('[data-column-id="' + columnId + '"] .board-cards');
        if (!column) return 0;
        const cards = column.querySelectorAll('.board-card');
        const y = DragDrop._lastY;
        for (let i = 0; i < cards.length; i++) {
            const rect = cards[i].getBoundingClientRect();
            const midY = rect.top + rect.height / 2;
            if (y < midY) return i;
        }
        return cards.length;
    },

    getColumnDropIndex: function (rowSelector) {
        const row = document.querySelector(rowSelector);
        if (!row) return 0;
        const columns = row.querySelectorAll('.board-column');
        const x = DragDrop._lastX;
        for (let i = 0; i < columns.length; i++) {
            const rect = columns[i].getBoundingClientRect();
            const midX = rect.left + rect.width / 2;
            if (x < midX) return i;
        }
        return columns.length;
    },

    clearDragData: function () {
        DragDrop._draggedCardId = null;
        DragDrop._draggedSourceColumnId = null;
        DragDrop._draggedColumnId = null;
        DragDrop._lastY = 0;
        DragDrop._lastX = 0;
        DragDrop._clearIndicators();
    },

    _clearIndicators: function () {
        document.querySelectorAll('.drop-indicator').forEach(function (el) { el.remove(); });
        document.querySelectorAll('.column-drop-indicator').forEach(function (el) { el.remove(); });
    },

    handleCardDragOver: function (columnId, y) {
        DragDrop._lastY = y;
        DragDrop._clearIndicators();
        const column = document.querySelector('[data-column-id="' + columnId + '"] .board-cards');
        if (!column) return;

        const cards = column.querySelectorAll('.board-card');
        let insertBefore = cards.length;
        for (let i = 0; i < cards.length; i++) {
            const rect = cards[i].getBoundingClientRect();
            const midY = rect.top + rect.height / 2;
            if (y < midY) {
                insertBefore = i;
                break;
            }
        }

        const indicator = document.createElement('div');
        indicator.className = 'drop-indicator';

        if (insertBefore < cards.length) {
            column.insertBefore(indicator, cards[insertBefore]);
        } else {
            column.appendChild(indicator);
        }
    },

    handleColumnDragOver: function (row, x) {
        DragDrop._lastX = x;
        DragDrop._clearIndicators();

        const columns = row.querySelectorAll('.board-column');
        let insertBefore = columns.length;
        for (let i = 0; i < columns.length; i++) {
            const rect = columns[i].getBoundingClientRect();
            const midX = rect.left + rect.width / 2;
            if (x < midX) {
                insertBefore = i;
                break;
            }
        }

        const indicator = document.createElement('div');
        indicator.className = 'column-drop-indicator';

        if (insertBefore < columns.length) {
            row.insertBefore(indicator, columns[insertBefore]);
        } else {
            row.appendChild(indicator);
        }
    }
};

DragDrop.init();
