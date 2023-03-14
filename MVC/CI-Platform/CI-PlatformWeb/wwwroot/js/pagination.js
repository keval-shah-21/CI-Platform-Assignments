function createPaginationHTML() {
    pagination.innerHTML = "";
    let totalMissions = $('#totalMissions').val();
    totalPages = Math.ceil(totalMissions / 9);
    if (totalPages > 5) {
        pagination.innerHTML = `<button data-page="previous" class="btn rounded border border-2">
        <img src="/images/static/previous.png" alt="previous">
        </button>`;
    }
    if (totalPages > 1) {
        pagination.innerHTML += `<button data-page="left" class="btn rounded border border-2">
        <img src="/images/static/left.png" alt="left"></button>`;
        for (let i = 1; i <= totalPages; i++) {
            pagination.innerHTML += `<button data-page="${i}"
             class="btn rounded border border-2 shadow-sm color-darkgray fw-light">${i}</button>`;
        }
        pagination.innerHTML += `<button data-page="right" class="btn rounded border border-2">
        <img src="/images/static/right-arrow1.png" alt="right"></button>`;
    }
    if (totalPages > 5) {
        pagination.innerHTML += `<button data-page="next" class="btn rounded border border-2">
        <img src="/images/static/next.png" alt="next">
        </button>`;
    }
    //event listner
    document.querySelectorAll('[data-page]').forEach((btn) => {
        $(btn).click(() => handlePagination($(btn).data('page')));

        if ($(btn).data('page') <= 5 * (pageSet - 1) || $(btn).data('page') > pageSet * 5)
            $(btn).addClass("d-none")
    });

    // css to current page
    $(`[data-page=${page}]`).addClass('active-page');
}
function scrollPageSet() {
    document.querySelectorAll('[data-page]').forEach((btn) => {
        if ($(btn).data('page') <= 5 * (pageSet - 1) || $(btn).data('page') > pageSet * 5)
            $(btn).addClass("d-none")
        else
            $(btn).removeClass("d-none");
    });
}
function handlePagination(value) {
    if (value == 'next') {
        if (pageSet * 5 > totalPages) return;
        pageSet += 1;
        scrollPageSet();
        return;
    }
    else if (value == 'previous') {
        if (pageSet == 1) return;
        pageSet -= 1;
        scrollPageSet();
        return;
    }
    else if (value == 'left') {
        if (page == 1) return;
        page = page - 1;
        if (page <= (pageSet - 1) * 5) {
            pageSet -= 1;
            scrollPageSet();
        }
    }
    else if (value == 'right') {
        if (page == totalPages) return;
        page = page + 1;
        if (page > pageSet * 5) {
            pageSet += 1;
            scrollPageSet();
        }
    }
    else {
        if (page == value) {
            window.scrollTo(0, 0);
            return;
        }
        $(`[data-page=${page}]`).removeClass('active-page');
        page = value;
    }
    MakeAjaxCall();
}