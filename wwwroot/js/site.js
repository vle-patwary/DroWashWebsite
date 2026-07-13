document.addEventListener('DOMContentLoaded', function () {
  var menuToggle = document.getElementById('menuToggle');
  var mainNav = document.getElementById('mainNav');
  var navOverlay = document.getElementById('navOverlay');

  if (menuToggle && mainNav) {
    function openMenu() {
      mainNav.classList.add('open');
      if (navOverlay) navOverlay.classList.add('open');
      document.body.classList.add('nav-open');
      menuToggle.setAttribute('aria-expanded', 'true');
    }

    function closeMenu() {
      mainNav.classList.remove('open');
      if (navOverlay) navOverlay.classList.remove('open');
      document.body.classList.remove('nav-open');
      menuToggle.setAttribute('aria-expanded', 'false');
    }

    menuToggle.addEventListener('click', function () {
      if (mainNav.classList.contains('open')) {
        closeMenu();
      } else {
        openMenu();
      }
    });

    mainNav.querySelectorAll('a').forEach(function (a) {
      a.addEventListener('click', closeMenu);
    });

    if (navOverlay) {
      navOverlay.addEventListener('click', closeMenu);
    }

    document.addEventListener('keydown', function (e) {
      if (e.key === 'Escape') closeMenu();
    });

    // If the viewport is resized back to desktop width while the menu
    // is open, reset the menu state so it doesn't get stuck.
    window.addEventListener('resize', function () {
      if (window.innerWidth > 920) closeMenu();
    });
  }

  // Get A Quote form
  var quoteForm = document.getElementById('quoteForm');
  if (quoteForm) {
    var submitBtn = document.getElementById('quoteSubmitBtn');
    var successBox = document.getElementById('quoteSuccess');
    var successName = document.getElementById('quoteSuccessName');

    function clearErrors() {
      quoteForm.querySelectorAll('.field-error').forEach(function (el) { el.textContent = ''; });
      quoteForm.querySelectorAll('.form-row').forEach(function (el) { el.classList.remove('has-error'); });
    }

    function showErrors(errors) {
      clearErrors();
      Object.keys(errors).forEach(function (field) {
        var errorEl = quoteForm.querySelector('.field-error[data-for="' + field + '"]');
        if (errorEl) {
          errorEl.textContent = errors[field][0];
          var row = errorEl.closest('.form-row');
          if (row) row.classList.add('has-error');
        }
      });
    }

    // Basic client-side checks so users get instant feedback before the
    // round trip to the server (which still re-validates everything).
    function validateClientSide(formData) {
      var errors = {};
      var required = {
        FullName: 'Please enter your name.',
        Email: 'Please enter an email address.',
        Phone: 'Please enter a phone number.',
        PropertyAddress: 'Please enter the property address.',
        PropertyType: 'Please select a property type.'
      };
      Object.keys(required).forEach(function (field) {
        var value = (formData.get(field) || '').toString().trim();
        if (!value) errors[field] = [required[field]];
      });
      var email = (formData.get('Email') || '').toString().trim();
      if (email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
        errors.Email = ['Please enter a valid email address.'];
      }
      return errors;
    }

    quoteForm.addEventListener('submit', function (e) {
      e.preventDefault();
      clearErrors();

      var formData = new FormData(quoteForm);
      var clientErrors = validateClientSide(formData);
      if (Object.keys(clientErrors).length > 0) {
        showErrors(clientErrors);
        return;
      }

      submitBtn.disabled = true;
      submitBtn.textContent = 'Sending...';

      fetch('/Quote/Submit', {
        method: 'POST',
        body: formData
      })
        .then(function (res) { return res.json(); })
        .then(function (data) {
          if (data.success) {
            var name = (formData.get('FullName') || '').toString().trim();
            successName.textContent = name ? ', ' + name : '';
            quoteForm.hidden = true;
            successBox.hidden = false;
          } else if (data.errors) {
            showErrors(data.errors);
          }
        })
        .catch(function () {
          var generalError = quoteForm.querySelector('.field-error[data-for="General"]');
          if (generalError) {
            generalError.textContent = 'Something went wrong. Please try again or call us directly.';
          }
        })
        .finally(function () {
          submitBtn.disabled = false;
          submitBtn.textContent = 'Get A Quote';
        });
    });
  }

  // Before/after slider
  var wrap = document.getElementById('compareWrap');
  if (wrap) {
    var handle = document.getElementById('compareHandle');
    var beforeLayer = wrap.querySelector('.before');
    var dragging = false;

    function setPos(clientX) {
      var rect = wrap.getBoundingClientRect();
      var pct = ((clientX - rect.left) / rect.width) * 100;
      pct = Math.max(0, Math.min(100, pct));
      beforeLayer.style.clipPath = 'inset(0 ' + (100 - pct) + '% 0 0)';
      handle.style.left = pct + '%';
    }

    wrap.addEventListener('mousedown', function (e) { dragging = true; setPos(e.clientX); });
    window.addEventListener('mousemove', function (e) { if (dragging) setPos(e.clientX); });
    window.addEventListener('mouseup', function () { dragging = false; });
    wrap.addEventListener('touchstart', function (e) { setPos(e.touches[0].clientX); }, { passive: true });
    wrap.addEventListener('touchmove', function (e) {
      setPos(e.touches[0].clientX);
      e.preventDefault();
    }, { passive: false });
  }

  // Hero photo slideshow
  var slideshow = document.getElementById('heroSlideshow');
  if (slideshow) {
    var slides = Array.prototype.slice.call(slideshow.querySelectorAll('.slide'));
    var dots = Array.prototype.slice.call(slideshow.querySelectorAll('.dot'));
    var prevBtn = document.getElementById('slidePrev');
    var nextBtn = document.getElementById('slideNext');
    var current = 0;
    var timer = null;
    var AUTOPLAY_MS = 5000;

    function goTo(index) {
      if (!slides.length) return;
      current = (index + slides.length) % slides.length;
      slides.forEach(function (s, i) { s.classList.toggle('active', i === current); });
      dots.forEach(function (d, i) { d.classList.toggle('active', i === current); });
    }

    function next() { goTo(current + 1); }
    function prev() { goTo(current - 1); }

    function startAutoplay() {
      stopAutoplay();
      if (slides.length > 1) timer = setInterval(next, AUTOPLAY_MS);
    }
    function stopAutoplay() {
      if (timer) clearInterval(timer);
    }

    if (slides.length > 1) {
      if (nextBtn) nextBtn.addEventListener('click', function () { next(); startAutoplay(); });
      if (prevBtn) prevBtn.addEventListener('click', function () { prev(); startAutoplay(); });
      dots.forEach(function (dot, i) {
        dot.addEventListener('click', function () { goTo(i); startAutoplay(); });
      });
      slideshow.addEventListener('mouseenter', stopAutoplay);
      slideshow.addEventListener('mouseleave', startAutoplay);
      startAutoplay();
    }
  }
});
