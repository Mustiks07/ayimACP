// Star rating interactive input
document.addEventListener('DOMContentLoaded', function () {
    // Auto-dismiss alerts
    setTimeout(function () {
        document.querySelectorAll('.auto-dismiss').forEach(function (el) {
            el.style.transition = 'opacity 0.5s';
            el.style.opacity = '0';
            setTimeout(() => el.remove(), 500);
        });
    }, 4000);

    // Quantity controls
    document.querySelectorAll('.qty-inc').forEach(btn => {
        btn.addEventListener('click', function () {
            var input = this.closest('.qty-control').querySelector('input');
            input.value = parseInt(input.value || 1) + 1;
            input.closest('form')?.submit();
        });
    });

    document.querySelectorAll('.qty-dec').forEach(btn => {
        btn.addEventListener('click', function () {
            var input = this.closest('.qty-control').querySelector('input');
            var val = parseInt(input.value || 1) - 1;
            if (val >= 0) {
                input.value = val;
                input.closest('form')?.submit();
            }
        });
    });
});
