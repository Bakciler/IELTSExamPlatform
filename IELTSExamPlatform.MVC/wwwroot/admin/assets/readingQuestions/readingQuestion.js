// Question type configurations
const questionTypes = {
    'choice-question': {
        title: 'Choice Question Type',
        question: 'Create your custom choice question'
    }
};

// Global variables
let currentQuestionType = 'choice-question';
let currentPassage = '';
let userAnswers = {};
let audioPlayer = null;
let optionCounter = 0;

// Initialize the page
document.addEventListener('DOMContentLoaded', function () {
    // Load initial question type
    setTimeout(() => {
        changeQuestionType();
    }, 100);

    // Initialize smooth scrolling
    initSmoothScrolling();

    // Initialize animations
    initScrollAnimations();

    // Initialize counter animations
    initCounterAnimations();
});

// Change passage selection
function changePassage() {
    const selectElement = document.getElementById('passageSelect');
    currentPassage = selectElement.value;

    if (currentPassage) {
        showNotification(`Selected passage: ${selectElement.options[selectElement.selectedIndex].text}`, 'info');

        // Update question type options based on passage
        updateQuestionTypeOptions(currentPassage);
    } else {
        showNotification('Please select a passage first.', 'warning');
    }
}

// Update question type options based on selected passage
function updateQuestionTypeOptions(passage) {
    const questionTypeSelect = document.getElementById('questionType');
    const currentValue = questionTypeSelect.value;

    // Clear existing options
    questionTypeSelect.innerHTML = '';

    let options = [];

    // Only choice question type available
    options = [
        { value: 'choice-question', text: 'Choice Question Type' }
    ];

    // Add options to select
    options.forEach(option => {
        const optionElement = document.createElement('option');
        optionElement.value = option.value;
        optionElement.textContent = option.text;
        questionTypeSelect.appendChild(optionElement);
    });

    // Set default value
    if (options.length > 0) {
        questionTypeSelect.value = options[0].value;
        currentQuestionType = options[0].value;
        changeQuestionType();
    }
}

// Change question type based on selection
// Bu function question type değiştiğinde çağrılır ve ilgili input alanlarını oluşturur
function changeQuestionType() {
    const selectElement = document.getElementById('questionType');
    currentQuestionType = selectElement.value;

    const container = document.getElementById('questionContainer');
    const questionData = questionTypes[currentQuestionType];

    if (!questionData) {
        container.innerHTML = '<div class="alert alert-warning">Question type not found.</div>';
        return;
    }

    // generateQuestionHTML function'ını çağırarak tüm input alanlarını oluşturur
    container.innerHTML = generateQuestionHTML(questionData);

    // Initialize question-specific functionality
    initializeQuestionType(currentQuestionType);
}

// Generate HTML for different question types
// Bu function ana HTML yapısını oluşturur ve choice question için gerekli input alanlarını ekler
function generateQuestionHTML(data) {
    let html = `
        <div class="question-container">
            <h6 class="fw-bold text-primary mb-3">${data.title}</h6>
    `;

    // Choice question type için question text input alanı oluşturur
    // Bu textarea kullanıcının soru metnini yazması için kullanılır
    if (currentQuestionType === 'choice-question') {
        html += `
            <div class="mb-4">
                <label class="form-label fw-bold">Question Text:</label>
                <textarea class="form-control" id="questionText" rows="3" placeholder="Enter your question text here..."></textarea>
            </div>
        `;
    } else {
        html += `<div class="question-text">${data.question}</div>`;
    }

    // Choice question için option alanlarını oluşturan function'ı çağırır
    html += generateChoiceQuestionHTML(data);

    // Save ve Reset butonlarını ekler
    html += `
        <div class="mt-4">
            <button class="btn btn-success me-2" onclick="saveQuestion()">
                <i class="fas fa-save me-1"></i>
                Save Question
            </button>
            <button class="btn btn-outline-secondary" onclick="resetQuestion()">
                <i class="fas fa-redo me-1"></i>
                Reset
            </button>
        </div>
    </div>
    `;

    return html;
}

// Generate HTML for choice question type
// Bu function choice question için option container'ını ve Add Option butonunu oluşturur
function generateChoiceQuestionHTML(data) {
    optionCounter = 0;
    let html = `
        <div class="choice-question-container">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h6 class="fw-bold mb-0">Answer Options:</h6>
                <button class="btn btn-primary btn-sm" onclick="addOption()">
                    <i class="fas fa-plus me-1"></i>
                    Add Option
                </button>
            </div>
            <div id="optionsContainer">
                <!-- Options will be dynamically added here -->
            </div>
        </div>
    `;

    // İlk option'ı otomatik olarak ekler
    addOption();

    return html;
}

// Add new option to choice question
// Bu function her yeni option için input alanlarını oluşturur:
// - Option harfi (A, B, C, D...)
// - Option text input alanı
// - Correct answer checkbox
// - Remove butonu
function addOption() {
    const container = document.getElementById('optionsContainer');

    if (!container) {
        console.error('Options container not found!');
        return;
    }

    // Alfabetik harf oluşturur (A, B, C, D...)
    const optionLetter = String.fromCharCode(65 + optionCounter);

    // Her option için HTML yapısını oluşturur
    const optionHTML = `
        <div class="option-row mb-3" data-option="${optionCounter}">
            <div class="row align-items-center">
                <div class="col-1">
                    <span class="option-letter fw-bold text-primary">${optionLetter}.</span>
                </div>
                <div class="col-8">
                    <input type="text" class="form-control option-text" placeholder="Enter option text..." data-option="${optionCounter}">
                </div>
                <div class="col-2">
                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" id="correct${optionCounter}" data-option="${optionCounter}">
                        <label class="form-check-label" for="correct${optionCounter}">
                            Correct
                        </label>
                    </div>
                </div>
                <div class="col-1">
                    <button class="btn btn-outline-danger btn-sm" onclick="removeOption(${optionCounter})" title="Remove option">
                        <i class="fas fa-trash"></i>
                    </button>
                </div>
            </div>
        </div>
    `;

    // HTML'i container'a ekler
    container.insertAdjacentHTML('beforeend', optionHTML);
    optionCounter++;

    // Option harflerini günceller
    updateOptionLetters();
}

// Remove option from choice question
// Bu function seçilen option'ı siler ve harfleri yeniden düzenler
function removeOption(optionIndex) {
    const optionRow = document.querySelector(`[data-option="${optionIndex}"]`);
    if (optionRow) {
        optionRow.remove();
        updateOptionLetters();
    }
}

// Update option letters after removal
// Bu function option silindikten sonra harfleri yeniden düzenler (A, B, C, D...)
function updateOptionLetters() {
    const optionRows = document.querySelectorAll('.option-row');
    optionRows.forEach((row, index) => {
        const letterSpan = row.querySelector('.option-letter');
        const letter = String.fromCharCode(65 + index);
        letterSpan.textContent = letter + '.';

        // Data attribute'ları günceller
        row.setAttribute('data-option', index);
        row.querySelector('.option-text').setAttribute('data-option', index);
        row.querySelector('.form-check-input').setAttribute('data-option', index);
        row.querySelector('.form-check-input').setAttribute('id', `correct${index}`);
        row.querySelector('.form-check-label').setAttribute('for', `correct${index}`);
        row.querySelector('.btn').setAttribute('onclick', `removeOption(${index})`);
    });

    optionCounter = optionRows.length;
}


// Initialize question-specific functionality
function initializeQuestionType(type) {
    // Only choice question type - no special initialization needed
}



// Reset question
function resetQuestion() {
    userAnswers[currentQuestionType] = {};

    // Clear question text for choice question type
    const questionTextArea = document.getElementById('questionText');
    if (questionTextArea) {
        questionTextArea.value = '';
    }

    // Clear all selections
    document.querySelectorAll('.option-item').forEach(item => {
        item.classList.remove('selected');
    });

    document.querySelectorAll('input[type="radio"]').forEach(radio => {
        radio.checked = false;
    });

    document.querySelectorAll('input[type="text"]').forEach(input => {
        input.value = '';
        input.style.borderBottomColor = '#4e73df';
    });

    document.querySelectorAll('textarea').forEach(textarea => {
        textarea.value = '';
    });

    document.querySelectorAll('.matching-item').forEach(item => {
        item.classList.remove('selected');
        item.style.background = '';
    });

    // Clear choice question options
    const optionsContainer = document.getElementById('optionsContainer');
    if (optionsContainer) {
        optionsContainer.innerHTML = '';
        optionCounter = 0;
        addOption(); // Add one default option
    }

    showNotification('Question reset successfully.', 'info');
}

// Button click handlers
function saveQuestion() {
    saveQuestionToBackend();
}

function collectFormData() {
    const formData = {
        passage: currentPassage,
        type: currentQuestionType,
        question: document.querySelector('.question-text')?.textContent || '',
        data: {}
    };

    switch (currentQuestionType) {
        case 'choice-question':
            const questionText = document.getElementById('questionText')?.value.trim() || '';
            const optionTexts = Array.from(document.querySelectorAll('.option-text')).map(input => input.value.trim());
            const correctAnswers = Array.from(document.querySelectorAll('.form-check-input:checked')).map(checkbox =>
                parseInt(checkbox.getAttribute('data-option'))
            );
            const passageId = document.getElementById('passageSelect')?.value || null;

            formData.data.passageId = passageId;
            formData.data.questionText = questionText;
            formData.data.options = optionTexts;
            formData.data.correctAnswers = correctAnswers;
            break;
    }

    return formData;
}

function validateFormData(formData) {
    if (!formData.passage) {
        showNotification('Please select a passage first.', 'error');
        return false;
    }

    // For choice question type, check questionText instead of question
    if (formData.type === 'choice-question') {
        if (!formData.data.questionText || formData.data.questionText.length === 0) {
            showNotification('Please enter question text.', 'error');
            return false;
        }
    } else {
        if (!formData.question) return false;
    }

    switch (formData.type) {
        case 'choice-question':
            return formData.data.questionText && formData.data.questionText.length > 0 &&
                formData.data.options && formData.data.options.length > 0 &&
                formData.data.options.every(option => option.length > 0) &&
                formData.data.correctAnswers && formData.data.correctAnswers.length > 0;
        default:
            return true;
    }
}

// Notification system
function showNotification(message, type = 'info') {
    // Remove existing notifications
    const existingNotifications = document.querySelectorAll('.notification');
    existingNotifications.forEach(notification => notification.remove());

    // Create notification element
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;

    const iconClass = {
        'success': 'fas fa-check-circle text-success',
        'error': 'fas fa-exclamation-circle text-danger',
        'warning': 'fas fa-exclamation-triangle text-warning',
        'info': 'fas fa-info-circle text-info'
    };

    notification.innerHTML = `
        <div class="notification-content">
            <i class="${iconClass[type]}"></i>
            <span>${message}</span>
            <button class="notification-close" onclick="this.parentElement.parentElement.remove()">
                <i class="fas fa-times"></i>
            </button>
        </div>
    `;

    // Add to page
    document.body.appendChild(notification);

    // Animate in
    setTimeout(() => {
        notification.classList.add('show');
    }, 100);

    // Auto remove after 4 seconds
    setTimeout(() => {
        if (notification.parentElement) {
            notification.classList.remove('show');
            setTimeout(() => {
                if (notification.parentElement) {
                    notification.remove();
                }
            }, 300);
        }
    }, 4000);
}

// Smooth scrolling
function initSmoothScrolling() {
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
}

// Scroll animations
function initScrollAnimations() {
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('fade-in-up');
            }
        });
    }, observerOptions);

    // Observe elements for scroll animations
    const animateElements = document.querySelectorAll('.feature-card, .section-card, .stat-item');
    animateElements.forEach(el => {
        observer.observe(el);
    });
}

// Counter animations
function initCounterAnimations() {
    const counters = document.querySelectorAll('.stat-number');

    const counterObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                animateCounters();
                counterObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.5 });

    const statsSection = document.querySelector('.py-5.bg-primary');
    if (statsSection) {
        counterObserver.observe(statsSection);
    }
}

function animateCounters() {
    const counters = document.querySelectorAll('.stat-number');

    counters.forEach(counter => {
        const target = counter.textContent;
        const isPercentage = target.includes('%');
        const isRating = target.includes('/');
        const isTime = target.includes('+') || target.includes('/');

        if (isTime) {
            return;
        }

        const numericValue = parseFloat(target.replace(/[^\d.]/g, ''));
        let current = 0;
        const increment = numericValue / 50;
        const timer = setInterval(() => {
            current += increment;
            if (current >= numericValue) {
                current = numericValue;
                clearInterval(timer);
            }

            if (isPercentage) {
                counter.textContent = Math.floor(current) + '%';
            } else if (isRating) {
                counter.textContent = (current / 10).toFixed(1) + '/5';
            } else {
                counter.textContent = Math.floor(current).toLocaleString() + '+';
            }
        }, 30);
    });
}

// Navbar scroll effect
window.addEventListener('scroll', () => {
    const navbar = document.querySelector('.navbar');
    if (window.scrollY > 100) {
        navbar.style.boxShadow = '0 2px 20px rgba(0, 0, 0, 0.1)';
    } else {
        navbar.style.boxShadow = 'none';
    }
});

// Error handling
window.addEventListener('error', (e) => {
    console.error('An error occurred:', e.error);
    showNotification('Something went wrong. Please try again.', 'error');
});

// Keyboard navigation
document.addEventListener('keydown', (e) => {
    // ESC key closes notifications
    if (e.key === 'Escape') {
        const notifications = document.querySelectorAll('.notification');
        notifications.forEach(notification => notification.remove());
    }

    // Enter key on buttons
    if (e.key === 'Enter' && e.target.classList.contains('btn')) {
        e.target.click();
    }
});


async function saveQuestionToBackend() {
    const formData = collectFormData(); // Hazırda sən yazmısan

    if (!validateFormData(formData)) return;

    // Choice question üçün dataları backend DTO formatına çeviririk
    const payload = {
        Type: "Choice", // backend switch-case-də istifadə olunacaq
        ReadingPassageId: formData.data.passageId,
        QuestionText: "salam",
        Order: 1, // Sən lazım gəlsə frontend-də user daxil edə bilər
        Options: formData.data.options.map((text, index) => ({
            Code: String.fromCharCode(65 + index), // A, B, C, D
            Content: text,
            IsCorrect: formData.data.correctAnswers.includes(index)
        }))
    };
    console.log(payload);
    try {
        const response = await fetch('/Admin/Reading/CreateQuestion', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify(payload)
        });

        const result = await response.json();

        if (result.success) {
            showNotification('Question saved successfully!', 'success');
            console.log('Saved Question ID:', result.questionId);

            // Reset the form after save
            resetQuestion();
        } else {
            showNotification(result.message || 'Failed to save question', 'error');
        }
    } catch (error) {
        console.error('Error saving question:', error);
        showNotification('Error connecting to server. Please try again.', 'error');
    }
}
// Button click handlers
//function saveQuestion() {
//    const questionData = questionTypes[currentQuestionType];
//    const formData = collectFormData();
    
//    if (validateFormData(formData)) {
//        showNotification('Question saved successfully!', 'success');
//        console.log('Question Data:', formData);
//    } else {
//        showNotification('Please fill in all required fields.', 'error');
//    }
//}


