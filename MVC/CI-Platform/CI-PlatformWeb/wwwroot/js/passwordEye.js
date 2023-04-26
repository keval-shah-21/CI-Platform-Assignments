document.querySelectorAll(".password-eye").forEach(eye => {
    $(eye).click(() => {
        const input = $(eye).prev();
        if (input.attr("type") == 'password')
            input.attr('type', 'text');
        else
            input.attr('type', 'password');
    })
})